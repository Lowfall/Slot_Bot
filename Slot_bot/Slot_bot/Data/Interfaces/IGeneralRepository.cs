
namespace Slot_bot.Data.Interfaces
{
    public interface IGeneralRepository<T>
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);
        public Task DeleteAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task AddAsync(T entity);
    }
}
