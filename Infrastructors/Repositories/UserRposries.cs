using Core;
using Infrastructors.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructors.Repositories
{
    internal class UserRposries(UsersDbCOntext context) : IRepository
    {
        private readonly UsersDbCOntext usersDbContext = context;

        public async Task<bool> CreateUserAsync(User user)
        {
            await usersDbContext.Users.AddAsync(user);
            await usersDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<User> EditUserAsync(User user)
        {
            var userInDb = usersDbContext.Users
                                         .Single(u => u.UserId == user.UserId)
                                         ?? throw new ArgumentNullException($"Пользователь не найден,с id {user.UserId}");
            
            userInDb.Email = user.Email;
            userInDb.LastName = user.LastName;
            userInDb.FirstName = user.FirstName;
            userInDb.Phone = user.Phone;
            userInDb.Password = user.Password;

            await usersDbContext.SaveChangesAsync();

            return userInDb;
        }

        public async Task<User> GetUserAsync(long userId) => await usersDbContext.Users
                                  .AsNoTracking()
                                  .SingleAsync(user => user.UserId == userId);

        public IQueryable<User> GetUsersAsync()
        {
            return usersDbContext.Users
                                       .AsNoTracking();
        }
    }
}
