using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Netflix_Clone.Data;
using Netflix_Clone.Models;

namespace Netflix_Clone.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly TMDBService _tmdbService;
        private readonly ApplicationDbContext _dbContext;

        public UserController(SignInManager<User> signInManager, UserManager<User> userManager, TMDBService tmdbService, ApplicationDbContext dbContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tmdbService = tmdbService;
            _dbContext = dbContext;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel model, string returnUrl = null)
        {
            User? user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == model.Email);
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (user == null) return NotFound();

            var result = await _signInManager.PasswordSignInAsync(
                user.UserName!, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Collection");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            User user = new User() { UserName = model.Username, Email = model.Email };
            var result = await _userManager.CreateAsync(user,  model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Collection");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return RedirectToAction("Register", "Home");


        }
        [HttpPost]

        [HttpPost]
        public async Task<IActionResult> AddMovieToList(AddToListModel model)
        { 
            //1: Get the current user
            var user = await _userManager.GetUserAsync(User);
            //2: Get the movie/show ID from the request
            Movie movie = await _tmdbService.GetMovie(model.MovieId);
            //3: Check if the movie/show already exists in the user's list
            bool result = await _dbContext.UserMovies
                .AnyAsync(um => um.UserId == user.Id && um.MovieId == model.MovieId);
            //4: If it doesn't exist, add it to the user's list
            UserMovie userMovie = new UserMovie() {
                MovieId = movie.Id,
                UserId = user.Id,
                AddedOn = DateTime.Now,
                Status = model.Status,
                Rating = model.Rating,
                Note = model.Note
            };
            if(!result)
            {
                _dbContext.UserMovies.Add(userMovie);
            }
            //5: Save changes to the database
            await _dbContext.SaveChangesAsync();
            //6: Return a success response
            return Ok();
        }
        public async Task<IActionResult> AddShowToList(AddToListModel model)
        {
            //1: Get the current user
            var user = await _userManager.GetUserAsync(User);
            //2: Get the show ID from the request
            Show show = await _tmdbService.GetShow(model.ShowId);
            //3: Check if the show + season already exists in the user's list
            bool result = await _dbContext.UserShows
                .AnyAsync(um => um.UserId == user.Id && um.ShowId == model.ShowId && um.SeasonId != model.SeasonId);
            //4: If it doesn't exist, add it to the user's list
            UserShow userMovie = new UserShow()
            {
                ShowId = show.Id,
                SeasonId = model.SeasonId,
                UserId = user.Id,
                AddedOn = DateTime.Now,
                Status = model.Status,
                Rating = model.Rating,
                EpisodesWatched = model.EpisodesWatched,
                Note = model.Note
            };
            if (!result)
            {
                _dbContext.UserShows.Add(userMovie);
            }
            //5: Save changes to the database
            await _dbContext.SaveChangesAsync();
            //6: Return a success response
            return Ok();
        }
        [Authorize]
        public async Task<IActionResult> GetMyListPage()
        {
            UserListModel<UserMovie, Movie> userMovies = await GetListMovies();
            UserListModel<UserShow, Show> userShows = await GetListShows();
            UserListViewModel model = new UserListViewModel
            {
                Movies = userMovies,
                Shows = userShows
            };
            return View("MyListPage", model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteFromList([FromBody] DeleteItemDto request)
        {
            var userId = _userManager.GetUserId(User);

            if (request.Type == "movie" && request.MovieId.HasValue)
            {
                var userMovie = await _dbContext.UserMovies
                    .FirstOrDefaultAsync(m => m.MovieId == request.MovieId.Value && m.UserId == userId);

                if (userMovie == null) return NotFound();

                _dbContext.UserMovies.Remove(userMovie);
            }
            else if (request.Type == "show" && request.ShowId.HasValue && request.SeasonId.HasValue)
            {
                var userShow = await _dbContext.UserShows
                    .FirstOrDefaultAsync(s => s.ShowId == request.ShowId.Value
                                           && s.SeasonId == request.SeasonId.Value
                                           && s.UserId == userId);

                if (userShow == null) return NotFound();

                _dbContext.UserShows.Remove(userShow);
            }
            else
            {
                return BadRequest("Invalid data.");
            }

            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        #region Helper Methods
        public async Task<UserListModel<UserMovie, Movie>> GetListMovies()
        {
            //Get User Movies
            List<UserMovie> userMovies = await _dbContext.UserMovies.Where(um => um.UserId == _userManager.GetUserId(User)).ToListAsync();
            List<Movie> movies = new List<Movie>();

            foreach (int movieId in userMovies.Select(um => um.MovieId))
            {
                Movie movie = await _tmdbService.GetMovie(movieId);
                if (movie != null)
                {
                    movies.Add(movie);
                }
            }
            return new UserListModel<UserMovie, Movie>
            {
                ListData = userMovies,
                CollectionData = movies
            };
        }
        public async Task<UserListModel<UserShow, Show>> GetListShows()
        {
            //Get User Movies
            List<UserShow> userShows = await _dbContext.UserShows.Where(um => um.UserId == _userManager.GetUserId(User)).ToListAsync();
            List<Show> shows = new List<Show>();

            foreach (int showId in userShows.Select(um => um.ShowId))
            {
                Show show = await _tmdbService.GetShow(showId);
                if (show != null)
                {
                    shows.Add(show);
                }
            }
            return new UserListModel<UserShow, Show>
            {
                ListData = userShows,
                CollectionData = shows
            };
        }
        #endregion
    }
}
