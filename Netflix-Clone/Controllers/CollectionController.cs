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

        [HttpPost]
        public async Task<IActionResult> Search()
        {
            return View();
        }

    }
}
