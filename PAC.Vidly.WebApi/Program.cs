using Microsoft.EntityFrameworkCore;
using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Movies;

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

var connectionString = configuration.GetConnectionString("Vidly");
if(string.IsNullOrEmpty(connectionString))
{
    throw new Exception("Missing BuildingManagerDb connection-string");
}

services.AddDbContext<DbContext, PacVidlyDbContext>
    (options => options.UseSqlite(connectionString));

services.AddScoped<IMovieService, MovieService>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
