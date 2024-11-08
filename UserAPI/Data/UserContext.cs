using Microsoft.EntityFrameworkCore;
using UserAPI.Models;


namespace UserAPI.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) // care i faza cu : base() ?
        {
            Database.EnsureCreated();
        }

        public DbSet<UserDTO> Users { get; set; } = null!;
        //public DbSet<DeviceDTO> Devices { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDTO>()
                .HasKey(u => u.Id); // Set GUID as primary key
            //modelBuilder.Entity<UserDTO>().HasMany(u => u.Devices).WithOne().HasForeignKey(d => d.UserId).IsRequired(false).OnDelete(DeleteBehavior.Cascade);
        }


    }
}
