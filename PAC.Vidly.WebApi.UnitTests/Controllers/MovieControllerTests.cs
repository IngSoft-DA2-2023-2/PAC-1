using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PAC.Vidly.WebApi.Controllers.Movies;
using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.UnitTests.Controllers
{
    [TestClass]
    public sealed class MovieControllerTests
    {
        private MovieController _controller;
        private Mock<IMovieService> _movieServiceMock;
        private Mock<HttpContext> _httpContextMock;

        private User _userLoggedForTest;

        [TestInitialize]
        public void Initialize()
        {
            _movieServiceMock = new Mock<IMovieService>(MockBehavior.Strict);
            _controller = new MovieController(_movieServiceMock.Object);
            
            _httpContextMock = new Mock<HttpContext>(MockBehavior.Strict);
            ControllerContext controllerContext = new ControllerContext
            {
                HttpContext = _httpContextMock.Object
            };
            _controller.ControllerContext = controllerContext;

            _userLoggedForTest = new User()
            {
                Name = "TEST",
                Email = "TEST",
                Password = "TEST"
            };
            _httpContextMock.SetupGet(c => c.Items[Items.UserLogged]).Returns(_userLoggedForTest);
        }

        #region Create
        [TestMethod]
        public void Create_WhenInfoIsCorrect_ShouldReturnId()
        {
            CreateMovieArgs request = new CreateMovieArgs("test");

            Movie expectedMovie = new Movie()
            {
                Name = "test",
            };
            
            _movieServiceMock.Setup(movieService =>
                movieService.Create(It.IsAny<CreateMovieArgs>(), It.IsAny<User>())).Returns(expectedMovie.Id);
            
            var id = _controller.Create(request);

            _movieServiceMock.VerifyAll();
            id.Should().NotBeNull(id);
            id.Should().Be(expectedMovie.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WhenNameIsEmpty_ShouldThrowException()
        {
            var repositoryMock = new Mock<IRepository<Movie>>(MockBehavior.Strict);
            MovieService service = new MovieService(repositoryMock.Object);
            MovieController controller = new MovieController(service);

            CreateMovieArgs request = new CreateMovieArgs("");
        }
        
        #endregion

        #region GetAll
        [TestMethod]
        public void GetAll_WhenExistOnlyOneMovie_ShouldReturnMoviesMapped()
        {
            Movie expectedMovie = new Movie()
            {
                Name = "test",
                CreatorId = "id",
                Creator = _userLoggedForTest
            };
            
            MovieBasicInfoResponse basicInfo = new MovieBasicInfoResponse(expectedMovie);
            List<MovieBasicInfoResponse> response = [basicInfo];
            
            _movieServiceMock.Setup(movieService =>
                movieService.GetAll()).Returns(response);
            
            var movies = _controller.GetAll();
            
            _movieServiceMock.VerifyAll();

            movies.Should().HaveCount(1);

            var movie = movies[0];
            movie.Name.Should().Be("test");
            movie.CreatorName.Should().Be(_userLoggedForTest.Name);
        }
        #endregion
    }
}
