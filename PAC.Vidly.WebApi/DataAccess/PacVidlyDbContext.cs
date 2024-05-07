﻿using Microsoft.EntityFrameworkCore;
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

        // public PacVidlyDbContext(DbContextOptions options) 
        //     : base(options) 
        // {
        // }
        
        public PacVidlyDbContext(DbContextOptions<PacVidlyDbContext> options) : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=vidly.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigSchema(modelBuilder);
            ConfigSeedData(modelBuilder);
        }

        private static void ConfigSchema(ModelBuilder modelBuilder)
        {
        }

        private static void ConfigSeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = "seedUserId",
                Email = "seed@user.com",
                Password = "SeedPassword",
                Name = "SeedName"
            });
        }
    }
}
