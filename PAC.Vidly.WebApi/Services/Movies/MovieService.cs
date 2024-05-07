﻿using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Movies.Entities;

namespace PAC.Vidly.WebApi.Services.Movies
{
    public sealed class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _movieRepository;

        public MovieService(IRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public string Create(Movie movie, string userLoggedId)
        {
            _movieRepository.Add(movie);
            return movie.Id;
        }

        public List<MovieBasicInfoResponse> GetAll()
        {
            List<Movie> movies = _movieRepository.GetAll();
            List<MovieBasicInfoResponse> basicInfo = new List<MovieBasicInfoResponse>();
            
            foreach (Movie movie in movies)
            {
                MovieBasicInfoResponse movieBasicInfo = new MovieBasicInfoResponse(movie);
            }

            return basicInfo;
        }
    }
}
