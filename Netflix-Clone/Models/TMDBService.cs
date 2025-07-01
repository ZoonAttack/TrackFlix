using Netflix_Clone.Data;
using Netflix_Clone.Data.DTOs;
using Netflix_Clone.Mappers;
using System.Text.Json;

namespace Netflix_Clone.Models
{
    public class TMDBService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl = "https://api.themoviedb.org/3";

        public TMDBService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["TMDB:ApiKey"];
        }

        public async Task<List<Movie>> GetTrendingMoviesAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/trending/movie/day?api_key={_apiKey}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var tmdbResponse = JsonSerializer.Deserialize<TMDBResponse<TMDBMovieDto>>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return tmdbResponse?.Results.Select(dto => dto.ToMovie()).ToList() ?? new List<Movie>();
        }


        public async Task<List<Show>> GetTrendingShowsAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/trending/tv/day?api_key={_apiKey}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var tmdbResponse = JsonSerializer.Deserialize<TMDBResponse<TMDBShowDto>>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return tmdbResponse?.Results.Select(dto => dto.ToShow()).ToList() ?? new List<Show>();
        }

        public async Task<List<Movie>> GetRecommendedMoviesAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/movie/top_rated?api_key={_apiKey}");
            var json = await response.Content.ReadAsStringAsync();
            var tmdbResponse = JsonSerializer.Deserialize<TMDBResponse<TMDBMovieDto>>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return tmdbResponse?.Results.Select(dto => dto.ToMovie()).ToList() ?? new List<Movie>();
        }

        public async Task<List<Show>> GetRecommendedShowsAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/tv/top_rated?api_key={_apiKey}");
            var json = await response.Content.ReadAsStringAsync();
            var tmdbResponse = JsonSerializer.Deserialize<TMDBResponse<TMDBShowDto>>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return tmdbResponse?.Results.Select(dto => dto.ToShow()).ToList() ?? new List<Show>();
        }

    }
}
