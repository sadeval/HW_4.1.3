using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AviationSystem
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Plane> Planes { get; set; }
        public DbSet<PlaneSpecification> PlaneSpecifications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=AviationSystem;Trusted_Connection=True;")
                .UseLazyLoadingProxies();
        }
    }
}
