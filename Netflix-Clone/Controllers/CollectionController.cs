using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Netflix_Clone.Data;
using Netflix_Clone.Models;

namespace Netflix_Clone.Controllers
{
    public class CollectionController : Controller
    {
        private readonly TMDBService _tmdbService;
        private readonly ApplicationDbContext _dbContext;
        public CollectionController(TMDBService tmdbService, ApplicationDbContext dbContext)
        {
            _tmdbService = tmdbService;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            HomeViewModel model = new()
            {
                TrendingMovies = await _tmdbService.GetTrendingMoviesAsync(),
                TrendingShows = await _tmdbService.GetTrendingShowsAsync(),
                RecommendedMovies = await _tmdbService.GetRecommendedMoviesAsync(),
                RecommendedShows = await _tmdbService.GetRecommendedShowsAsync()
            };
            return View(model);
        }
        public async Task<IActionResult> GetMovieDetails(int movieId)
        {
            Movie model = await _tmdbService.GetMovie(movieId);
            return PartialView("GetMovieDetailsPartial", model);
        }
        public async Task<IActionResult> GetShowDetails(int showId)
        {
            Show model = await _tmdbService.GetShow(showId);
            return PartialView("GetShowDetailsPartial", model);
        }

        public async Task<IActionResult> GetMoviesPage(int page = 1)
        {
            string query = $"page={page}";
            MoviesPageViewModel model = new MoviesPageViewModel
            {
                Genres = await _tmdbService.GetGenresAsync(),
                Movies = await _tmdbService.GetMoviesAsync(query),
                Page = page
            };
            return View("MoviesPage", model);
        }
        public async Task<IActionResult> GetShowsPage(int page = 1)
        {
            string query = $"page={page}";
            ShowsPageViewModel model = new ShowsPageViewModel
            {
                Genres = await _tmdbService.GetGenresAsync(),
                Shows = await _tmdbService.GetShowsAsync(query),
                Page = page
            };
            return View("ShowsPage", model);
        }
        public async Task<IActionResult> AddMovieToList(int movieId)
        {
            AddToListViewModel model = new AddToListViewModel
            {
                Movie = await _tmdbService.GetMovie(movieId),
                FormModel = new AddToListModel()
                {
                    MovieId = movieId
                }
            };
            return PartialView("AddMovieToListPartial", model);
        }
        public async Task<IActionResult> AddShowToList(int showId)
        {
            AddToListViewModel model = new AddToListViewModel
            {
                Show = await _tmdbService.GetShow(showId),
                FormModel = new AddToListModel()
                {
                    ShowId = showId
                }
            };
            return PartialView("AddShowToListPartial", model);
        }

        public async Task<IActionResult> GetMoviePage(int movieId)
        {
            List<UserMovie> usersWithRating = _dbContext.UserMovies.Where(um => um.MovieId == movieId && um.Rating != 0.0).Include(u => u.User).ToList();
            List<Tuple<string, float, string>> userRatings = new();
            foreach (var user in usersWithRating)
            {
                //Get names and ratings for that movie
                userRatings.Add(Tuple.Create(user.User.UserName, user.Rating, user.Note)!);
            }
            

            CollectionItemPageViewModel<Movie> model = new CollectionItemPageViewModel<Movie>
            {
                CollectionItem = await _tmdbService.GetMovie(movieId),
                UsersRating = userRatings
            };
            return View("MoviePage", model);
        }

        public async Task<IActionResult> GetShowPage(int showId)
        {
            List<UserShow> usersWithRating = _dbContext.UserShows.Where(um => um.ShowId == showId && um.Rating != 0.0).Include(u => u.User).ToList();

            List<Tuple<string, float, string>> userRatings = new();
            foreach (var user in usersWithRating)
            {
                //Get names and ratings for that movie
                userRatings.Add(Tuple.Create(user.User.UserName, user.Rating, user.Note)!);
            }


            CollectionItemPageViewModel<Show> model = new CollectionItemPageViewModel<Show>
            {
                CollectionItem = await _tmdbService.GetShow(showId),
                UsersRating = userRatings
            };
            return View("ShowPage", model);
        }

        public async Task<IActionResult> Search(string query, int page = 1)
        {
            //1-Get all movie results
            List<Movie> movies = await _tmdbService.SearchMovies(query, page);
            //2-Get all show results
            List<Show> shows = await _tmdbService.SearchShows(query, page);
            //3-Put them in one View Model
            SearchViewModel model = new SearchViewModel
            {
                Movies = movies,
                Shows = shows,
                Query = query
            };
            //4-Return that model to a new "SearchPage" view with pages and all
            return View("SearchPage", model);
        }

        //public async Task<IActionResult> FilterMovies()
        //{
        //    // Implement filtering logic here
        //}
        //public async Task<IActionResult> FilterShows()
        //{
        //    // Implement filtering logic here
        //}
    }
   
}
