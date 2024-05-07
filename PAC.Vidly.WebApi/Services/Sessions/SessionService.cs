using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Sessions.Entities;
using PAC.Vidly.WebApi.Services.Users;
using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.Services.Sessions
{
    public sealed class SessionService : ISessionService
    {
        private readonly IRepository<Session> _sessionRepository;

        private readonly IUserService _userService;

        public SessionService(
            IRepository<Session> sessionRepository,
            IUserService userService)
        {
            _sessionRepository = sessionRepository;
            _userService = userService;
        }

        public string Create(string email, string password)
        {
            var userLogged = _userService.GetByCredentials(email, password);

            var sessionSaved = _sessionRepository.GetOrDefault(s => s.UserId == userLogged.Id);

            if (sessionSaved != null)
            {
                sessionSaved.Token = Guid.NewGuid().ToString().Replace("-","");

                _sessionRepository.Update(sessionSaved);

                return sessionSaved.Token;
            }

            var session = new Session(userLogged.Id);

            _sessionRepository.Add(session);

            return session.Token;
        }

        public bool IsValidToken(string token)
        {
            var isValid = Guid.TryParse(token, out Guid _);
            
            return isValid;
        }

        public User GetUserByToken(string token)
        {
            var session = _sessionRepository.GetOrDefault(s => s.Token == token);

            if (session == null)
            {
                throw new InvalidOperationException("Token expired");
            }

            return session.User;
        }

    }
}
