﻿using System.Linq.Expressions;
using FluentAssertions;
using Moq;
using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Sessions;
using PAC.Vidly.WebApi.Services.Sessions.Entities;
using PAC.Vidly.WebApi.Services.Users;
using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.UnitTests.Services
{
    [TestClass]
    public sealed class SessionServiceTests
    {
        private Mock<IRepository<Session>> _sessionRepositoryMock;
        private IUserService _userService;
        private SessionService _service;

        [TestInitialize]
        public void Initialize()
        {
            _sessionRepositoryMock = new Mock<IRepository<Session>>(MockBehavior.Strict);
            _userService = new UserService(new Mock<IRepository<User>>().Object);
            _service = new SessionService(_sessionRepositoryMock.Object, _userService);
        }

        #region IsTokenValid
        [TestMethod]
        public void IsValidToken_WhenTokenValidFormat_ShouldReturnTrue()
        {
            string token = Guid.NewGuid().ToString();

            var isValid = _service.IsValidToken(token);

            isValid.Should().BeTrue();
        }
        #endregion
        
        #region Get
        [TestMethod]
        public void GetUserByToken_WhenTokenIsValid_ShouldReturnUser()
        {
            var token = Guid.NewGuid().ToString();
            var session = new Session("test")
            {
                Token = token
            };

            _sessionRepositoryMock.Setup(r => r.GetOrDefault(It.IsAny<Expression<Func<Session, bool>>>())).Returns(session);
            var user = _service.GetUserByToken(token);

            user.Should().NotBeNull();
            user.Id.Should().Be(session.UserId);
        }

        #endregion
    }
}
