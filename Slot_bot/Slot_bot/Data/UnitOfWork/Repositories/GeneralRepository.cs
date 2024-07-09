using Microsoft.EntityFrameworkCore;
using Slot_bot.Data.Interfaces;

namespace Slot_bot.Data.UnitOfWork.Repositories
{
    public class GeneralRepository<T> : IGeneralRepository<T> where T : class
    {
        public ApplicationDbContext dbContext;
        public DbSet<T> table;
        public GeneralRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            table = this.dbContext.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
             table.Add(entity);
             await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            table.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await table.AsNoTracking().ToListAsync<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await table.FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            table.Update(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}
