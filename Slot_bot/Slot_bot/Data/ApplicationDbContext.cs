using Microsoft.EntityFrameworkCore;
using Slot_bot.Entities;

namespace Slot_bot.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
                
        }
    }
}
