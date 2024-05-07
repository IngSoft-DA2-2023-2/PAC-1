using Microsoft.EntityFrameworkCore;
using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Movies;
using Vidly.WebApi.Filters;

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


var _connectionString = configuration.GetConnectionString("MantenimientoEdificios");
if (string.IsNullOrEmpty(_connectionString))
{
    throw new Exception("Missing connection string");
}

services.AddDbContext<DbContext, PacVidlyDbContext>(options => options.UseSqlite(_connectionString));

services.AddScoped<IMovieService, MovieService>();

services.AddScoped<AuthenticationFilterAttribute>();

var app = builder.Build();
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();