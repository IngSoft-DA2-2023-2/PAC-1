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
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WhenNameIsNull_ShouldThrowException()
        {
            var args = new CreateMovieArgs(null);
            
            User userLogged = new User();

            try
            {
                _service.Create(args, userLogged);
            }
            catch (Exception ex)
            {
                ex.Message.Should().Be("Name cannot be empty or null");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Create_WhenMovieIsDuplicated_ShouldThrowException()
        {
            var args = new CreateMovieArgs("duplicated");
            
            User userLogged = new User();

            Movie excpected = new Movie()
            {
                Name = "duplicated"
            };
            
            _movieRepositoryMock.Setup(movieRepo =>
                movieRepo.GetOrDefault(It.IsAny<Expression<Func<Movie, bool>>>())).Returns(excpected);
            
            _service.Create(args, userLogged);
        }
        
        #endregion

        #region Success
        [TestMethod]
        public void Create_WhenInfoIsCorrect_ShouldReturnId()
        {
            var args = new CreateMovieArgs("duplicated");

            User userLogged = new User();
            
            _movieRepositoryMock.Setup(movieRepo => movieRepo.Add(It.IsAny<Movie>()));
            
            _movieRepositoryMock.Setup(movieRepo =>
                movieRepo.GetOrDefault(It.IsAny<Expression<Func<Movie, bool>>>())).Returns((Movie?)null);
            var movieId = _service.Create(args, userLogged);

            _movieRepositoryMock.VerifyAll();
            movieId.Should().NotBeNull();
        }
        
        #endregion
        #endregion
    }
}
