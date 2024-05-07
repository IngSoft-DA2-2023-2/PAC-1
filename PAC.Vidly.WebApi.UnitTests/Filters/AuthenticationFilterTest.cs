using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using Moq;
using PAC.Vidly.WebApi.Filters;
using PAC.Vidly.WebApi.Services.Sessions;
using PAC.Vidly.WebApi.Services.Sessions.Entities;
using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.UnitTests.Filters
{

    [TestClass]
    public class AuthenticationFilterTest
    {
        private Mock<HttpContext> _httpContextMock;
        private AuthenticationFilterAttribute _attribute;
        private AuthorizationFilterContext _context;

        public AuthenticationFilterTest()
        {
            _attribute = new AuthenticationFilterAttribute();
        }

        [TestInitialize]
        public void Initialize()
        {
            _httpContextMock = new Mock<HttpContext>(MockBehavior.Strict);
            _httpContextMock.SetupGet(h => h.Items[Items.UserLogged]).Returns(new User());
            _context = new AuthorizationFilterContext(
                new ActionContext(
                    _httpContextMock.Object,
                    new RouteData(),
                    new ActionDescriptor()
                ),
                new List<IFilterMetadata>()
            );
        }

        #region Error

        [TestMethod]
        public void OnAuthorization_WhenEmptyHeaders_ShouldRerturnUnauthenticatedResponse()
        {
            _httpContextMock.Setup(h => h.Request.Headers).Returns(new HeaderDictionary());

            _attribute.OnAuthorization(_context);

            var response = _context.Result;

            response.Should().NotBeNull();
            var concreteResponse = response as ObjectResult;
            concreteResponse.Should().NotBeNull();
            concreteResponse.StatusCode.Should().Be((int)HttpStatusCode.Unauthorized);
            GetInnerCode(concreteResponse.Value).Should().Be("Unauthenticated");
            GetMessage(concreteResponse.Value).Should().Be("You are not authenticated");
        }

        [TestMethod]
        public void OnAuthorization_WhenAuthorizationIsEmpty_ShouldRerturnUnauthenticatedResponse()
        {
            _httpContextMock.Setup(h => h.Request.Headers).Returns(
                new HeaderDictionary(new Dictionary<string, StringValues> { { "Authorization", string.Empty } }));

            _attribute.OnAuthorization(_context);

            var response = _context.Result;

            response.Should().NotBeNull();
            var concreteResponse = response as ObjectResult;
            concreteResponse.Should().NotBeNull();
            concreteResponse.StatusCode.Should().Be((int)HttpStatusCode.Unauthorized);
            GetInnerCode(concreteResponse.Value).Should().Be("Unauthenticated");
            GetMessage(concreteResponse.Value).Should().Be("You are not authenticated");
        }

        [TestMethod]
        public void OnAuthorization_WhenAuthorizationFormatIsInvalid_ShouldReturnInvalidAuthorizationResponse()
        {
            _httpContextMock.Setup(h => h.Request.Headers).Returns(
                new HeaderDictionary(new Dictionary<string, StringValues> { { "Authorization", "Invalid_token" } }));

            _attribute.OnAuthorization(_context);

            var response = _context.Result;

            response.Should().NotBeNull();
            var concreteResponse = response as ObjectResult;
            concreteResponse.Should().NotBeNull();
            concreteResponse.StatusCode.Should().Be((int)HttpStatusCode.Unauthorized);
            GetInnerCode(concreteResponse.Value).Should().Be("InvalidAuthorization");
            GetMessage(concreteResponse.Value).Should().Be("The provided authorization header format is invalid");
        }

        [TestMethod]
        public void
            OnAuthorization_WhenAuthorizationCorrectButNotExistSession_ShouldReturnInvalidAuthorizationResponse()
        {
            var token = Guid.NewGuid().ToString();

            var sessionServiceMock = new Mock<ISessionService>();

            _httpContextMock.Setup(h => h.RequestServices.GetService(typeof(ISessionService)))
                .Returns(sessionServiceMock.Object);

            _httpContextMock.SetupSet(h => h.Items[Items.UserLogged] = (User?)null);

            sessionServiceMock.Setup(s => s.GetUserByToken(token)).Returns((User?)null);

            _httpContextMock.Setup(h => h.Request.Headers)
                .Returns(new HeaderDictionary(new Dictionary<string, StringValues> { { "Authorization", token } }));

            _attribute.OnAuthorization(_context);

            var response = _context.Result;

            response.Should().NotBeNull();
            var concreteResponse = response as ObjectResult;
            concreteResponse.Should().NotBeNull();
            concreteResponse.StatusCode.Should().Be((int)HttpStatusCode.Unauthorized);
            GetInnerCode(concreteResponse.Value).Should().Be("Unauthenticated");
            GetMessage(concreteResponse.Value).Should().Be("You are not authenticated");
        }

        [TestMethod]
        public void OnAuthorization_WhenAuthorizationCorrect_ShouldNotReturnInvalidAuthorizationResponse()
        {
            var token = Guid.NewGuid().ToString();

            var sessionServiceMock = new Mock<ISessionService>();

            var user = new User()
            {
                Name = "Pepe1",
                Email = "Pepe@gmail.com",
                Password = "Pepito241",
            };

            _httpContextMock.Setup(h => h.RequestServices.GetService(typeof(ISessionService)))
                .Returns(sessionServiceMock.Object);

            sessionServiceMock.Setup(s => s.GetUserByToken(token)).Returns(user);

            _httpContextMock.SetupSet(h => h.Items[Items.UserLogged] = user);

            _httpContextMock.Setup(h => h.Request.Headers)
                .Returns(new HeaderDictionary(new Dictionary<string, StringValues> { { "Authorization", token } }));

            _attribute.OnAuthorization(_context);
        }

        #endregion

        private string GetInnerCode(object value)
        {
            return value.GetType().GetProperty("InnerCode").GetValue(value).ToString();
        }

        private string GetMessage(object value)
        {
            return value.GetType().GetProperty("Message").GetValue(value).ToString();
        }
    }
}