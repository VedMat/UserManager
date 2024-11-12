using Microsoft.EntityFrameworkCore;
using UserManager.Models;
using System.Collections.Generic;
using UserManager.Models;

namespace UserManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Startup> Startups { get; set; }
        public DbSet<StartupProgram> StartupPrograms { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Location> Locations { get; set; }
    }
}
