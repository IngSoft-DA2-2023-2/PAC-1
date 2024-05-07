using PAC.Vidly.WebApi.Filters;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Sessions;
using PAC.Vidly.WebApi.Services.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>();
});

var services = builder.Services;
var configuration = builder.Configuration;

services.AddScoped<IUserService, UserService>();
services.AddScoped<ISessionService, SessionService>();
services.AddScoped<IMovieService, MovieService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
