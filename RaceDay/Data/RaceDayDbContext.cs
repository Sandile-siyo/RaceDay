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


        public DbSet<Event> Events { get; set; }


        public DbSet<Category> Categories { get; set; }


        public DbSet<Registration> Registrations { get; set; }


        public DbSet<Result> Results { get; set; }


        public DbSet<Weather> Weathers { get; set; }


        public DbSet<RaceDay.Models.Route> Routes { get; set; }
    }//end of the class
}//end of the namespace
   
