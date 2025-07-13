using Application.DTO;
using Core; 
namespace Application.Interfaces
{
    public interface IUserManager
    {
        public Task<User> GetUserAsync();

        public Task<IEnumerable<User>> GetUsersAsync();

        public Task<bool> CreateUserAsync(UserDTO user);

        public Task<User> EditUserAsync(UserDTO user);
        Task<bool> LoginUserAsync(string email, string password);
        Task<bool> LogOutAsync();
    }
}
