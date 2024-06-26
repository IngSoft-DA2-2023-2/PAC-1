﻿using PAC.Vidly.WebApi.Services.Movies.Entities;

namespace PAC.Vidly.WebApi.Controllers.Movies.Models
{
    public sealed record class MovieBasicInfoResponse
    {
        public string Id { get; init; }

        public string Name { get; init; }

        public string CreatorName {  get; init; }

        public MovieBasicInfoResponse(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            CreatorName = movie.Creator.Name;
        }
    }
}
