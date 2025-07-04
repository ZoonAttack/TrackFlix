using Microsoft.AspNetCore.Mvc;
using Netflix_Clone.Data;
using Netflix_Clone.Models;

namespace Netflix_Clone.Controllers
{
    public class CollectionController : Controller
    {
        private readonly TMDBService _tmdbService;

        public CollectionController(TMDBService tmdbService)
        {
            _tmdbService = tmdbService;
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

        public async Task<IActionResult> GetMoviesPage(string option = "")
        {
            MoviesPageViewModel model = new MoviesPageViewModel
            {
                Genres = await _tmdbService.GetGenresAsync(),
                Movies = await _tmdbService.GetMoviesAsync(option)
            };
            return View("MoviesPage", model);
        }
        public async Task<IActionResult> GetShowsPage(int page = 1)
        {
            string query = $"?page={page}";
            ShowsPageViewModel model = new ShowsPageViewModel
            {
                Genres = await _tmdbService.GetGenresAsync(),
                Shows = await _tmdbService.GetShowsAsync(query)
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
        //public async Task<IActionResult> AddedToList()
        //{

        //}

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
