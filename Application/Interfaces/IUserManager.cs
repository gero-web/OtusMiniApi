using Application.DTO;
using Core; 
namespace Application.Interfaces
{
    public interface IUserManager
    {
        public Task<IEnumerable<User>> GetUsersAsync(); 
        Task<bool> LoginUserAsync(string email, string password);
        Task<bool> LogOutAsync();
        Task<User> GetUserAsync();
        Task<User> EditUserAsync(UserDTO userDTO);
        Task<bool> CreateUserAsync(UserDTO user);
    }
}
