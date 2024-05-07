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
            _movieRepositoryMock = new Mock<IRepository<Movie>>(MockBehavior.Strict);
            _service = new MovieService(_movieRepositoryMock.Object);
        }

        #region Create
        #region Error
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WhenNameIsNull_ShouldThrowException()
        {
            CreateMovieArgs args = new CreateMovieArgs(
                null
            );
            
            var userLoggedId = "test2";

            _service.Create(args, userLoggedId);
        }

        /*[TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
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
                _service.Create(args, userLoggedId);
            }
            catch (Exception ex)
            {
                ex.Message.Should().Be("Movie is duplicated");
            }
        }*/
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WhenNameIsEmpty_ShouldThrowException()
        {
            CreateMovieArgs args = new CreateMovieArgs(
                string.Empty
            );
            
            var userLoggedId = "test2";

            _service.Create(args, userLoggedId);
        }
        #endregion

        #region Success
        [TestMethod]
        public void Create_WhenInfoIsCorrect_ShouldReturnId()
        {
            CreateMovieArgs args = new CreateMovieArgs(
                "test"
            );
            
            var userLoggedId = "test2";

            _movieRepositoryMock.Setup(userRepository => userRepository.Add(It.IsAny<Movie>()));
            
            var movieCreated = _service.Create(args, userLoggedId);

            _movieRepositoryMock.VerifyAll();
            movieCreated.Id.Should().NotBeNull();
            movieCreated.Name.Should().Be(args.Name);
        }
        #endregion
        #endregion
    }
}
