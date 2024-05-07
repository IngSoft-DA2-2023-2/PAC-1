using Microsoft.EntityFrameworkCore;
using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Sessions;
using PAC.Vidly.WebApi.Services.Sessions.Entities;
using PAC.Vidly.WebApi.Services.Users;
using PAC.Vidly.WebApi.Services.Users.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder
    .Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });



var services = builder.Services;
var configuration = builder.Configuration;

services.AddScoped<IRepository<Movie>, Repository<Movie>>();
services.AddScoped<IMovieService, MovieService>();

services.AddScoped<IRepository<User>, Repository<User>>();
services.AddScoped<ISessionService, SessionService>();

services.AddScoped<IRepository<Session>, Repository<Session>>();
services.AddScoped<IUserService, UserService>();

var PacVidlyConnectionString = configuration.GetConnectionString("Vidly");
if (string.IsNullOrEmpty(PacVidlyConnectionString))
{
    throw new Exception("Connection string not found");
}

services.AddDbContext<DbContext, PacVidlyDbContext>(options =>
  options.UseSqlServer(PacVidlyConnectionString));



var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
