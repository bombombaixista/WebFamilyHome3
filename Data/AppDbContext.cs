using Microsoft.EntityFrameworkCore;
using WebFamilyHome.Models;

namespace WebFamilyHome.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
    }
}
