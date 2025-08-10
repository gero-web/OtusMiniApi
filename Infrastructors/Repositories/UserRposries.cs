using Core;
using Infrastructors.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructors.Repositories
{
    internal class UserRposries(UsersDbCOntext context) : IRepository
    {
        private readonly UsersDbCOntext usersDbContext = context;

        public async Task<string> CreateOrderAsync(Order order)
        {
            var user = await usersDbContext.Users.SingleOrDefaultAsync(user => user.Id == order.UserId);
            var msg = "Не удалось, создать заказ";
       
            if (user.Balance >= order.OrderPrace)
            {
                msg = "Заказ создан";
              
                usersDbContext.Notifications.Add(new Notifications()
                {
                    User = user,
                    Notification = "Счастье",
                });

                user.Balance -= order.OrderPrace;
                usersDbContext.Orders.Add(order);
            }
            else
            {
                usersDbContext.Notifications.Add(new Notifications()
                {
                    User = user,
                    Notification = "Горе",
                });
            }

            await usersDbContext.SaveChangesAsync();
            return msg;
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            await usersDbContext.Users.AddAsync(user);
            await usersDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<User> EditUserAsync(User user, string userId)
        {
            var userInDb = usersDbContext.Users
                                         .Single(u => u.Id == userId)
                                         ?? throw new ArgumentNullException($"Пользователь не найден,с id {user.Id}");

            userInDb.Email = user.Email;
            userInDb.LastName = user.LastName;
            userInDb.FirstName = user.FirstName;
            userInDb.Phone = user.Phone;
            userInDb.Password = user.Password;

            await usersDbContext.SaveChangesAsync();

            return userInDb;
        }

        public IQueryable<Notifications> GetAllNotificationsUsers(string getUserId)
        {
            return usersDbContext.Notifications.AsNoTracking()
                                        .Where(note => note.UserId == getUserId);
        }

        public async Task<User> GetUserAsync(string userId) => await usersDbContext.Users
                                  .AsNoTracking()
                                  .SingleAsync(user => user.Id == userId);

        public IQueryable<User> GetUsersAsync()
        {
            return usersDbContext.Users
                                 .AsNoTracking();
        }
    }
}
