using Microsoft.AspNetCore.Mvc;
using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.Controllers
{
    public class VidlyController : ControllerBase
    {

        public VidlyController()
        {
        }
        protected User GetUserLogged()
        {
            var userLogged = HttpContext.Items[Items.UserLogged];

            var userLoggedMapped = (User)userLogged;

            return userLoggedMapped;
        }
    }
}
