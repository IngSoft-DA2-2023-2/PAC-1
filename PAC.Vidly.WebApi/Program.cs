using Microsoft.EntityFrameworkCore;
using PAC.Vidly.WebApi.Controllers.Sessions;
using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Sessions;
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

var vidlyConnectionString = configuration.GetConnectionString("Vidly");
if (string.IsNullOrEmpty(vidlyConnectionString))
{
    throw new Exception("Missing Vidly connection string");
}

services.AddScoped<DbContext, PacVidlyDbContext>().use); ;

services.AddScoped<IMovieService, MovieService>();

services.AddScoped<ISessionService, SessionService>();

services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
