using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Slot_bot.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        IConfigurationRoot configuration;

        public ApplicationDbContextFactory(IConfigurationRoot configuration)
        {
            this.configuration = configuration;
        }
        public ApplicationDbContextFactory()
        {
                
        }
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = configuration.GetConnectionString("MySqlConnection");
            builder.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString));

            return new ApplicationDbContext(builder.Options);
        }
    }
}
