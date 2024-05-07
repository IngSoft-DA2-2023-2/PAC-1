using System.Linq.Expressions;
using FluentAssertions;
using Moq;
using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Users.Entities;

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
            _movieRepositoryMock = new Mock<IRepository<Movie>>(MockBehavior.Strict);
            _service = new MovieService(_movieRepositoryMock.Object);
        }

        #region Create
        #region Error

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Create_WhenMovieIsDuplicated_ShouldThrowException()
        {
            var args = new CreateMovieArgs("duplicated");

            try
            {
                _movieRepositoryMock.Setup(r => r.GetOrDefault(It.IsAny<Expression<Func<Movie, bool>>>())).Returns(new Movie());
                
                _service.Create(args, new User());
            }
            catch (Exception ex)
            {
                ex.Message.Should().Be("Movie duplicated");
            }
        }
        #endregion

        #region Success
        [TestMethod]
        public void Create_WhenInfoIsCorrect_ShouldReturnId()
        {
            var args = new CreateMovieArgs("Correct");

            var movie = _service.Create(args, new User());

            _movieRepositoryMock.VerifyAll();
            movie.Should().NotBeNull();
            movie.Name.Should().Be("Correct");
        }
        #endregion
        #endregion
    }
}
