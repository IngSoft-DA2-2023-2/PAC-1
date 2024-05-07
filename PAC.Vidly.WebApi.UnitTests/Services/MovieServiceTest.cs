using FluentAssertions;
using Moq;
using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Arguments;
using PAC.Vidly.WebApi.Services.Movies.Entities;

namespace PAC.Vidly.WebApi.UnitTests.Services
{
    [TestClass]
    public sealed class MovieServiceTest
    {
        private Mock<IRepository<Movie>> _movieRepositoryMock;

        private MovieService _service;

        [TestInitialize]
        public void Initialize()
        {
            _movieRepositoryMock = new Mock<IRepository<Movie>>();
            _service = new MovieService(_movieRepositoryMock.Object);
        }
        
        [TestMethod]
        public void Create_WhenInfoIsCorrect_ShouldCallRepositoryAdd()
        {
            var request = new CreateMovieArgs(){ Name = "test", CreatorId = "1"};

            _service.Create(request);

            _movieRepositoryMock.Verify(x => x.Add(It.IsAny<Movie>()), Times.Once);
        }
    }
}
