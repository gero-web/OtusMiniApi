
using Core;

namespace Infrastructors.Repositories
{
    public interface IRepository
    {
        Task<User> GetUserAsync(string userId);

        IQueryable<User> GetUsersAsync();
        
        Task<bool> CreateUserAsync(User user);

        Task<User> EditUserAsync(User user, string userId);
        Task<String> CreateOrderAsync(Order order);
        IQueryable<Notifications> GetAllNotificationsUsers(string getUserId);
    }
}