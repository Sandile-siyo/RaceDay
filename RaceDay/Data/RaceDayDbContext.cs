using Microsoft.EntityFrameworkCore;
using RaceDay.Models;

namespace RaceDay.Data
{//start of the namespace

    public class RaceDayDbContext : DbContext
    {//start of the class

        public RaceDayDbContext(DbContextOptions<RaceDayDbContext> options)
            : base(options)
        {//start of the constructor

        }//end of the constructor


        // User table
        public DbSet<User> Users { get; set; }

        // Event table
        public DbSet<Event> Events { get; set; }

        // Category table
        public DbSet<Category> Categories { get; set; }

        // Registration table
        public DbSet<Registration> Registrations { get; set; }

        // Result table
        public DbSet<Result> Results { get; set; }

        // Weather table
        public DbSet<Weather> Weathers { get; set; }

        // Route table
        public DbSet<RaceDay.Models.Route> Routes { get; set; }

    }//end of the class

}//end of the namespace