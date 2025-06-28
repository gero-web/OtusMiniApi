using Application.DTO;
using Core; 
namespace Application.Interfaces
{
    public interface IUser
    {
        public Task<User> GetUserAsync(long userId);

        public Task<IEnumerable<User>> GetUsersAsync();

        public Task<bool> CreateUserAsync(UserDTO user);

        public Task<User> EditUserAsync(User user);
    
    }
}
