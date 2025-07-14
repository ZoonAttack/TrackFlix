﻿using Microsoft.IdentityModel.Tokens;
using TrackFlix.Data;
using TrackFlix.Data.DTOs;
using TrackFlix.Data.Utility;

namespace TrackFlix.Mappers
{
    public static class TMDBResponseMapper
    {
        public static Movie ToMovie(this TMDBMovieDto dto)
        {
            return new Movie
            {
                Id = dto.Id,
                Title = dto.Title,
                Language = dto.Original_Language,
                Description = dto.Overview,
                PosterURL = $"https://image.tmdb.org/t/p/w300{dto.Poster_Path}",
                ReleaseDate = dto.Release_Date,
                Rating = dto.Vote_Average,
                Genres = dto.Genres,
                GenreIds = dto.Genre_Ids,
                Actors = new List<string>(), // optional default
                UserMovies = new List<UserMovie>()
            };
        }
        public static Show ToShow(this TMDBShowDto dto)
        {
            return new Show
            {
                Id = dto.Id,
                Title = dto.Name,
                Language = dto.Original_Language,
                Description = dto.Overview,
                PosterURL = $"https://image.tmdb.org/t/p/w300{dto.Poster_Path}",
                SeasonsCount = dto.Number_Of_Seasons,
                Seasons = dto.Seasons.ToSeasonList(),
                ReleaseDate = dto.First_Air_Date,
                Rating = dto.Vote_Average,
                Genres = dto.Genres,
                GenreIds = dto.Genre_Ids,
                Actors = new List<string>(), // optional default
                UserShows = new List<UserShow>()
            };
        }
        public static List<Season> ToSeasonList(this List<TMDBSeasonDto> dto)
        {
            return dto.IsNullOrEmpty() ? new List<Season>() :  dto.Select(season => season.ToSeason()).ToList();

        }
        public static Season ToSeason(this TMDBSeasonDto dto)
        {
            return new Season
            {
                Id = dto.Id,
                Title = dto.Name,
                Description = dto.Overview,
                PosterURL = $"https://image.tmdb.org/t/p/w300{dto.Poster_Path}",
                ReleaseDate = dto.Air_Date,
                Rating = dto.Vote_Average,
                SeasonNumber = dto.Season_Number,
                EpisodesCount = dto.Episode_Count,
            };
        }
    }

}
