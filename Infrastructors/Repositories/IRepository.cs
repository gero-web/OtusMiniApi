
using Core;

namespace Infrastructors.Repositories
{
    public interface IRepository
    {
        Task<User> GetUserAsync(long userId);

        IQueryable<User> GetUsersAsync();
        
        Task<bool> CreateUserAsync(User user);

        Task<User> EditUserAsync(User user);
    }
}