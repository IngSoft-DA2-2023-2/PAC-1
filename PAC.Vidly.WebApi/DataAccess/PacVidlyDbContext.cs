using Microsoft.EntityFrameworkCore;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Sessions.Entities;
using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.DataAccess
{
    public sealed class PacVidlyDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        public DbSet<Session> Sessions { get; set; }

        public DbSet<User> Users { get; set; }

        public PacVidlyDbContext(DbContextOptions options) 
            : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigSchema(modelBuilder);
            ConfigSeedData(modelBuilder);
        }

        private static void ConfigSchema(ModelBuilder modelBuilder)
        {
            var userBuilder = modelBuilder
                .Entity<User>()
                .HasMany(u => u.Movies)
                .WithMany(m => m.Users)
                .UsingEntity<UserMovie>(
                    r => r.HasOne(x => x.Movie).WithMany().HasForeignKey(x => x.MovieId),
                    l => l.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId));
            
        }

        private static void ConfigSeedData(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasData(
                    new User
                    {
                        Name = "Admin Admin",
                        Email = "admin@gmail.com",
                        Password = "123456"
                    }
                );
        }
    }
    
    public sealed record class UserMovie
    {
        public string UserId { get; init; } = null!;

        public User User { get; init; } = null!;

        public string MovieId { get; init; } = null!;

        public Movie Movie { get; init; } = null!;
    }
}
