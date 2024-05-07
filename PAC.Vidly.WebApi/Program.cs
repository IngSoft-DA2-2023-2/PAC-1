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

builder.Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

var services = builder.Services;
var configuration = builder.Configuration;

var WebApiConnectionString = configuration.GetConnectionString("Vidly");
if (string.IsNullOrEmpty(WebApiConnectionString))
{
    throw new Exception("Missing WebApi connection string");
}

services.AddDbContext<DbContext, PacVidlyDbContext>(options => options.UseSqlite(WebApiConnectionString));

services.AddScoped<IRepository<User>, Repository<User>>();
services.AddScoped<IUserService, UserService>();

services.AddScoped<IRepository<Session>, Repository<Session>>();
services.AddScoped<ISessionService, SessionService>();

services.AddScoped<IRepository<Movie>, Repository<Movie>>();
services.AddScoped<IMovieService, MovieService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
