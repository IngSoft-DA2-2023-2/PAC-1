
using System.Security.Claims;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PAC.Vidly.WebApi.Controllers.Movies;
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


        [TestInitialize]
        public void Initialize()
        {
            _movieServiceMock = new Mock<IMovieService>(MockBehavior.Strict);
            _controller = new MovieController(_movieServiceMock.Object);
            _httpContextMock = new Mock<HttpContext>(MockBehavior.Strict);
            _httpContextMock.SetupGet(httpContext => httpContext.User)
                .Returns(new ClaimsPrincipal());
            
            _httpContextMock.SetupGet(httpContext => httpContext.Items[Items.UserLogged])
                .Returns(new User { Name = "S",
                    Id = "test",
                    Password = "SDDASDADSA",
                    Email = "dsadas@gmail.com"});
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = _httpContextMock.Object
            };
            
            _movieServiceMock.Setup(service => service.GetAll())
                .Returns(new List<Movie>()); 
            _movieServiceMock.Setup(service => service.Create(It.IsAny<Movie>(), It.IsAny<string>()));

            
        }

        #region Create
        [TestMethod]
        public void Create_WhenInfoIsCorrect_ShouldReturnId()
        {
            var request = new Movie
            {
                Id = "test",
                Name = "test",
                CreatorId = "test",
            };
            
            _movieServiceMock.Setup(service => service.Create(It.IsAny<Movie>(), It.IsAny<string>()))
                .Callback<Movie, string>((movie, id) => 
                {
                    _movieServiceMock.Setup(service => service.GetAll())
                        .Returns(new List<Movie> { movie });
                });

            var id = _controller.Create(request);

            var movies = _controller.GetAll();
            var movieId = movies[0].Id;

            _movieServiceMock.VerifyAll();
            id.Should().NotBeNull();
            id.Should().Be(movieId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WhenNameIsEmpty_ShouldThrowException()
        {
            var repositoryMock = new Mock<IRepository<Movie>>(MockBehavior.Strict);
            var service = new MovieService(repositoryMock.Object);

            var httpContextMock = new Mock<HttpContext>(MockBehavior.Strict);
            httpContextMock.SetupGet(httpContext => httpContext.User)
                .Returns(new ClaimsPrincipal());

            httpContextMock.SetupGet(httpContext => httpContext.Items[Items.UserLogged])
                .Returns(new User { Name = "S",
                    Id = "test",
                    Password = "SDDASDADSA",
                    Email = "dsadas@gmail.com"});

            var controller = new MovieController(service)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = httpContextMock.Object
                }
            };

            var request = new Movie
            {
                Id = "test",
                Name = string.Empty,
                CreatorId = "test",
            };

            try
            {
                controller.Create(request);
            }
            catch (Exception ex)
            {
                ex.Message.Should().Be("Name cannot be empty");
            }
        }
        #endregion

        #region GetAll
        [TestMethod]
        public void GetAll_WhenExistOnlyOneMovie_ShouldReturnMoviesMapped()
        {
            var repositoryMock = new Mock<IRepository<Movie>>(MockBehavior.Strict);
            var service = new MovieService(repositoryMock.Object);
            var controller = new MovieController(service);
            
            var movies = _controller.GetAll();

            _movieServiceMock.VerifyAll();

            movies.Should().HaveCount(1);

            var movie = movies[0];
            movie.Name.Should().Be("test");
            movie.Creator.Name.Should().Be("test creator");
        }
        #endregion
    }
}
