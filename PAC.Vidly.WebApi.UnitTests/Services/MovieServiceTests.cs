using System.Linq.Expressions;
using FluentAssertions;
using Moq;
using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;

namespace PAC.Vidly.WebApi.UnitTests.Services
{
    [TestClass]
    public sealed class MovieServiceTests
    {
        private Mock<IRepository<Movie>> _movieRepositoryMock;

        private MovieService _service;

        [TestInitialize]
        public void Initialize()
        {
            _movieRepositoryMock = new Mock<IRepository<Movie>>();
            _service = new MovieService(_movieRepositoryMock.Object);
        }

        #region Create
        #region Error

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Create_WhenMovieIsDuplicated_ShouldThrowException()
        {
            var args = new Movie
            {
                Id = "test",
                Name = "duplicated",
                CreatorId = "test"
            };
            var userLoggedId = "test2";

            try
            {
                _movieRepositoryMock.Setup(r => r.GetOrDefault(It.IsAny<Expression<Func<Movie, bool>>>()))
                    .Returns(args);

                //_service.Create(args, userLoggedId);
                _service.Create(args);
            }
            catch (Exception ex)
            {
                ex.Message.Should().Be("movie duplicated");
                throw;
            }
        }
        #endregion

        #region Success
        [TestMethod]
        public void Create_WhenInfoIsCorrect_ShouldReturnId()
        {
            var args = new Movie
            {
                Id = "test",
                Name = "duplicated",
                CreatorId = "test"
            };
            var userLoggedId = "test2";

            //var movieId = _service.Create(args, userLoggedId);
            var movieId = _service.Create(args);

            _movieRepositoryMock.VerifyAll();
            movieId.Should().NotBeNull();
            movieId.Should().Be(args.Id);
        }
        #endregion
        #endregion
    }
}
