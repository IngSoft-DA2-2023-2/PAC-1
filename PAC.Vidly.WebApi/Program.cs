using Microsoft.EntityFrameworkCore;
using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

builder.Services.AddControllers();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>();
});

var services = builder.Services;
services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});
var configuration = builder.Configuration;

var edifyConnectionString = configuration.GetConnectionString("Edify");
if (string.IsNullOrEmpty(edifyConnectionString))
{
    throw new Exception("Missing Edify connection string");
}

var app = builder.Build();

services.AddDbContext<DbContext, PacVidlyDbContext>(options => options.UseSqlServer(edifyConnectionString));

services.AddScoped<IRepository<Movie>, Repository<Movie>>();
services.AddScoped<IMovieService, MovieService>();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
