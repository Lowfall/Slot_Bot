using Slot_bot.Data.Interfaces;
using Slot_bot.Entities;

namespace Slot_bot.Data.UnitOfWork.Repositories
{
    public class UserRepository : GeneralRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
                
        }

        public async Task<bool> IsUserExistAsync(ulong id)
        {
            return table.Find(id) is null ? false : true;
        }
    }
}
