/****
    Application Database Context

**/

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApiPSCourse.Data.Entities;

namespace WebApiPSCourse.Data
{
    public class CampContext : DbContext
    {
        private readonly IConfiguration _config;

        public CampContext(DbContextOptions options, IConfiguration config) : base(options)
        {
            _config = config;
        }

        public DbSet<Camp> Camps { get; set; }

        public DbSet<Speaker> Speakers { get; set; }

        public DbSet<Talk> Talks { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("ApplicationConnection"));
        }

        // Seed initial data
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Camp>()
                .HasData(new
                {
                    CampId = 1,
                    Moniker = "LAG2019",
                    Name = "Made in Lagos Camp",
                    EventDate = new DateTime(2019, 10, 10),
                    LocationId = 1,
                    Length = 1
                });

            builder.Entity<Location>()
                .HasData(new
                {
                    LocationId = 1,
                    VenueName = "Lagos Conservation Centre",
                    Address1 = "123 Ikorodu Road",
                    CityTown = "Ikorodu",
                    StateProvince = "LA",
                    PostalCode = "123123",
                    Country = "Nigeria"
                });

            builder.Entity<Talk>()
                .HasData(new
                {
                    TalkId = 1,
                    CampId = 1,
                    SpeakerId = 1,
                    Title = "Sample Data Made Easy",
                    Abstract = "Thinking of good sample data example is tiring.",
                    Level = 200
                },
                new
                {
                    TalkId = 2,
                    CampId = 1,
                    SpeakerId = 2,
                    Title = "Entity Framework from Scratch",
                    Abstract = "Entity Framework from scratch in an hour.",
                    Level = 100
                });

            builder.Entity<Speaker>()
                .HasData(new
                {
                    SpeakerId = 1,
                    FirstName = "Ebuka",
                    LastName = "Abraham",
                    BlogUrl = "https://github.com/buka4rill",
                    Company = "Buka Investments",
                    CompanyUrl = "http://bukainvestments.com",
                    Twitter = "buka4rill"
                },
                new
                {
                    SpeakerId = 2,
                    FirstName = "Ada",
                    LastName = "Lovelace",
                    BlogUrl = "https://adalovelace.com",
                    Company = "Women in Tech LLC",
                    CompanyUrl = "http://womenintech.in",
                    Twitter = "adalovelace"
                });

        }

    }
}