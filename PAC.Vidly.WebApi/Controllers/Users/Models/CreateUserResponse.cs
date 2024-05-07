namespace PAC.Vidly.WebApi.Controllers.Users.Models
{
    public class CreateUserResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public CreateUserResponse(string id, string userName)
        {
            Id = id;
            UserName = userName;
        }
    }

   
}
