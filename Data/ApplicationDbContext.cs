using Microsoft.EntityFrameworkCore;
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
        public DbSet<Startup> Startups { get; set; }
        public DbSet<StartupProgram> StartupPrograms { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
}
