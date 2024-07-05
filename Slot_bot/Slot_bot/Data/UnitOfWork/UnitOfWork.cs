using Slot_bot.Data.Interfaces;
using Slot_bot.Data.UnitOfWork.Repositories;

namespace Slot_bot.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        ApplicationDbContext dbContext;
        IUserRepository userRepository;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IUserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(dbContext);
                return userRepository;
            }
        }

        public async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync(); 
            throw new NotImplementedException();
        }
    }
}
