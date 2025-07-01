using Netflix_Clone.Data;
using Netflix_Clone.Data.DTOs;
using Netflix_Clone.Data.Utility;

namespace Netflix_Clone.Mappers
{
    public static class TMDBResponseMapper
    {
        public static Movie ToMovie(this TMDBMovieDto dto)
        {
            return new Movie
            {
                Title = dto.Title,
                Language = dto.Original_Language,
                Description = dto.Overview,
                PosterURL = $"https://image.tmdb.org/t/p/w300{dto.Poster_Path}",
                ReleaseDate = dto.Release_Date,
                Rating = dto.Vote_Average,
                Actors = new List<string>(), // optional default
                Genre = Genre.None,
                UserMovies = new List<UserMovie>()
            };
        }
        public static Show ToShow(this TMDBShowDto dto)
        {
            return new Show
            {
                Title = dto.Name,
                Language = dto.Original_Language,
                Description = dto.Overview,
                PosterURL = $"https://image.tmdb.org/t/p/w300{dto.Poster_Path}",
                ReleaseDate = dto.First_Air_Date,
                Rating = dto.Vote_Average,
                Actors = new List<string>(), // optional default
                Genre = Genre.None,
                UserShows = new List<UserShow>()
            };
        }
    }

}
