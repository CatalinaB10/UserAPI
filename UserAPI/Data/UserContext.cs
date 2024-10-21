using DeviceAPI.Models;
using Microsoft.EntityFrameworkCore;
using UserAPI.Models;

namespace UserAPI.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) // care i faza cu : base() ?
        {

        }

        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Device> Devices { get; set; } = null!;

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(u => u.Devices).WithOne().HasForeignKey(d => d.UserId);
        }
    }
}
