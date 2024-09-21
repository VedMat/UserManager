using Microsoft.EntityFrameworkCore;
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
    }
}
