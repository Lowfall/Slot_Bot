using Slot_bot.Entities;

namespace Slot_bot.Data.Interfaces
{
    public interface IUserRepository : IGeneralRepository<User>
    {
        Task<bool> IsUserExistAsync(ulong id);
    }
}
