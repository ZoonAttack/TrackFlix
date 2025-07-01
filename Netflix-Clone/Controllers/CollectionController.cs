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

        [HttpPost]
        public async Task<IActionResult> Search()
        {
            return View();
        }

    }
}
