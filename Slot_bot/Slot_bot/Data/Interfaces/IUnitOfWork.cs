
namespace Slot_bot.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        Task SaveAsync();
    }
}
