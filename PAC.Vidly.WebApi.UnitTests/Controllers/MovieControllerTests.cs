
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PAC.Vidly.WebApi.Controllers.Movies;
using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Sessions;
using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.UnitTests.Controllers
{
    [TestClass]
    public sealed class MovieControllerTests
    {
        private MovieController _controller;
        private Mock<IMovieService> _movieServiceMock;
        private Mock<HttpContext> _httpContextMock;
        private User _user;

        [TestInitialize]
        public void Initialize()
        {
            _movieServiceMock = new Mock<IMovieService>(MockBehavior.Strict);
            _controller = new MovieController(_movieServiceMock.Object);
            _httpContextMock = new Mock<HttpContext>(MockBehavior.Strict);
            ControllerContext controllerContext = new ControllerContext { HttpContext = _httpContextMock.Object };
            _controller.ControllerContext = controllerContext;
            
            _user = new User
            {
                Name = "Juan",
                Email = "jm@email.com",
                Password = "jmPASS",
            };
            
            _httpContextMock.SetupGet(c => c.Items[Items.UserLogged]).Returns(_user);
        }

        #region Create
        [TestMethod]
        public void Create_WhenInfoIsCorrect_ShouldReturnId()
        {
            CreateMovieRequest request = new CreateMovieRequest
            {
                Name = "test",
            };
            
             Movie expectedMovie= new Movie
            {
                Name = request.Name
            };
             
            _movieServiceMock.Setup(i => i.Create(It.IsAny<CreateMovieArgs>(), It.IsAny<string>())).Returns(expectedMovie);
            MovieBasicInfoResponse response = _controller.Create(request);

            _movieServiceMock.VerifyAll();
            response.Id.Should().NotBeNull();
            response.Id.Should().Be(expectedMovie.Id);
        }
        #endregion

        #region GetAll
        /*[TestMethod]
        public void GetAll_WhenExistOnlyOneMovie_ShouldReturnMoviesMapped()
        {
            List<Movie> movies = new List<Movie>();
            
            CreateMovieRequest request = new CreateMovieRequest
            {
                Name = "test",
            };
            
            Movie expectedMovie= new Movie
            {
                Name = request.Name
            };
            
            movies.Add(expectedMovie);
            _movieServiceMock.Setup(i => i.GetAll(It.IsAny<User>(), It.IsAny<string?>())).Returns(movies);
            GetMoviesResponse response = _controller.GetAll();

            _movieServiceMock.VerifyAll();

            movies.Count.Should().Be(1);

            var movie = movies[0];
            movie.Name.Should().Be("test");
            movie.CreatorName.Should().Be("test creator");
        }*/
        #endregion
    }
}
