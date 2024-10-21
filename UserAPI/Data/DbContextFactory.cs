using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace UserAPI.Data
{
    public class DbContextFactory : IDesignTimeDbContextFactory<UserContext>
    {
        public UserContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UserContext>();

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var conn = configuration.GetConnectionString("DemoContext");

            optionsBuilder.UseNpgsql(conn);
            return new UserContext(optionsBuilder.Options);
        }
    }
}
