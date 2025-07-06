using Microsoft.Extensions.Options;
using Netflix_Clone.Data;
using Netflix_Clone.Data.DTOs;
using Netflix_Clone.Data.Utility;
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

        public async Task<List<Movie>> GetMoviesAsync(string options)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/discover/movie?api_key={_apiKey}&{options}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var tmdbResponse = JsonSerializer.Deserialize<TMDBResponse<TMDBMovieDto>>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return tmdbResponse?.Results.Select(dto => dto.ToMovie()).ToList() ?? new List<Movie>();
        }
        public async Task<List<Show>> GetShowsAsync(string options)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/discover/tv?api_key={_apiKey}&{options}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var tmdbResponse = JsonSerializer.Deserialize<TMDBResponse<TMDBShowDto>>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return tmdbResponse?.Results.Select(dto => dto.ToShow()).ToList() ?? new List<Show>();
        }
        internal async Task<List<Genre>> GetGenresAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/genre/movie/list?api_key={_apiKey}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var tmdbResponse = JsonSerializer.Deserialize<TMDBGenreDto>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return tmdbResponse.Genres ?? new TMDBGenreDto().Genres;
        }

        public async Task<Movie> GetMovie(int movieId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/movie/{movieId}?api_key={_apiKey}");
            var json = await response.Content.ReadAsStringAsync();
            Movie movie = JsonSerializer.Deserialize<TMDBMovieDto>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!.ToMovie();
            return movie;
        }
        public async Task<Show> GetShow(int showId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/tv/{showId}?api_key={_apiKey}");
            var json = await response.Content.ReadAsStringAsync();
            Show show = JsonSerializer.Deserialize<TMDBShowDto>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!.ToShow();
            return show;
        }

    }
}
