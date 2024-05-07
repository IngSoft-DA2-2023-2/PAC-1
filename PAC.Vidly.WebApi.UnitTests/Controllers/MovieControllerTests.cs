
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
        private User creator;

        [TestInitialize]
        public void Initialize()
        {
            _httpContextMock = new Mock<HttpContext>(MockBehavior.Strict);
            ControllerContext controllerContext = new ControllerContext { HttpContext = _httpContextMock.Object };
            _movieServiceMock = new Mock<IMovieService>(MockBehavior.Strict);
            _controller = new MovieController(_movieServiceMock.Object);
            _controller.ControllerContext = controllerContext;

            creator = new User()
            {
                Name = "creatorTest"
            };
        }

        #region Create
        [TestMethod]
        public void Create_WhenInfoIsCorrect_ShouldReturnId()
        {
            var request = new CreateMovieRequest()
            {
                Name = "test",
            };
            
            var movie = new Movie()
            {
                Name = "test",
            };

            _httpContextMock.SetupGet(c => c.Items[Items.UserLogged]).Returns(creator);
            _movieServiceMock.Setup(m => m.Create(It.IsAny<CreateMovieArgs>(), It.IsAny<User>())).Returns(movie);

            var response = _controller.Create(request);

            _movieServiceMock.VerifyAll();
            response.Id.Should().NotBeNull();
        }

        [TestMethod]
        public void Create_WhenNameIsEmpty_ShouldThrowException()
        {
            var request = new CreateMovieRequest()
            {
                Name = string.Empty,
            };

            try
            {
                _controller.Create(request);
            }
            catch (Exception ex)
            {
                ex.Message.Should().Be("Name cannot be empty or null");
            }
        }
        
        [TestMethod]
        public void Create_WhenNameIsNull_ShouldThrowException()
        {
            var request = new CreateMovieRequest()
            {
                Name = null,
            };

            _httpContextMock.SetupGet(c => c.Items[Items.UserLogged]).Returns(creator);
            
            try
            {
                _controller.Create(request);
            }
            catch (Exception ex)
            {
                ex.Message.Should().Be("Name cannot be empty or null");
            }
        }
        
        #endregion

        #region GetAll
        [TestMethod]
        public void GetAll_WhenExistOnlyOneMovie_ShouldReturnMoviesMapped()
        {

            _movieServiceMock.VerifyAll();

            _movieServiceMock.Setup(c => c.GetAll()).Returns(new List<Movie>() { new Movie()
            {
                Name = "test",
                Creator = creator,
            } });
         
            var movies = _controller.GetAll();
            
            movies.Should().HaveCount(1);

            var movie = movies[0];
            movie.Name.Should().Be("test");
            movie.CreatorName.Should().Be("creatorTest");
        }
        #endregion
    }
}
