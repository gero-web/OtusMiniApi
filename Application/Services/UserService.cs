using Application.DTO;
using Application.Interfaces;
using Core;
using Infrastructors.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    class UserService(IRepository userRepository) : IUser
    {
        private readonly IRepository repository = userRepository;

        public async Task<bool> CreateUserAsync(UserDTO user)
        {
            var userEn = new User {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                Phone = user.Phone,
                Email = user.Email,

            };

            return await repository.CreateUserAsync(userEn);
        }

        public async Task<User> EditUserAsync(User user)
        {
            return await repository.EditUserAsync(user);
        }

        public async Task<User> GetUserAsync(long userId)
        {
            return await repository.GetUserAsync(userId);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var users = repository.GetUsersAsync();

            return await users.ToListAsync();
        }
    }
}
