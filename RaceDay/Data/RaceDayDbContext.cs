using Microsoft.EntityFrameworkCore;
using RaceDay.Models;

namespace RaceDay.Data
{
    public class RaceDayDbContext : DbContext
    {
        public RaceDayDbContext(DbContextOptions<RaceDayDbContext> options)
            : base(options)
        {
        }

        // User table
        public DbSet<User> Users { get; set; }


        public DbSet<Event> Events { get; set; }


        public DbSet<Category> Categories { get; set; }


        public DbSet<Registration> Registrations { get; set; }


        public DbSet<Result> Results { get; set; }


        public DbSet<Weather> Weathers { get; set; }


        public DbSet<RaceDay.Models.Route> Routes { get; set; }
    }
}  
   
