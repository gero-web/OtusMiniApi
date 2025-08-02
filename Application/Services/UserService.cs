using Application.DTO;
using Application.Interfaces;
using Core;
using Infrastructors.Repositories; 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Application.Services
{
    class UserService(IRepository userRepository,
                      UserManager<User> userManager,
                      SignInManager<User> signInManager,
                      ILogger<UserService> logger,
                      IHttpContextAccessor httpContextAccessor) : IUserManager
    {
        private readonly IHttpContextAccessor _contextAccessor = httpContextAccessor;
        private readonly IRepository repository = userRepository;
        private readonly UserManager<User> _userManager = userManager;
        private readonly SignInManager<User> signInManager = signInManager;
        private readonly ILogger<UserService> _logger = logger;


        public bool IsAuthenticated => _contextAccessor.HttpContext?.User?
                                                       .Identity?.IsAuthenticated ?? false;
        public string GetUserId =>  Guid.NewGuid().ToString();
        public async Task<bool> CreateUserAsync(UserDTO user)
        {
            var userForCreation = new User()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Password = user.Password,
            };

            var resutl = await _userManager.CreateAsync(userForCreation, userForCreation.Password);

            if (!resutl.Succeeded)
            {
                var errorsDescriptions = string.Join("\n;", resutl.Errors.ToList().Select(x => x.Description));
                throw new Exception(errorsDescriptions);
            }

            return resutl.Succeeded;
        }

        public async Task<User> EditUserAsync(UserDTO userDTO)
        {
            var userId = GetUserId;
            var user = new User()
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Email = userDTO.Email,
                Password = userDTO.Password,
                UserName = userDTO.FirstName,
            };
            return await repository.EditUserAsync(user, userId);
        }

        public async Task<User> GetUserAsync()
        {
            _logger.LogInformation("test ");

            throw new Exception("test"); 
         //   return await repository.GetUserAsync();
        }

        public async Task<bool> LoginUserAsync(string userName, string password)
        {
            var result = await signInManager.PasswordSignInAsync(userName, password, false, false);
            if (!result.Succeeded)
            {
                throw new Exception($"Вход не выполнен, побробуйте снова");
            }

            return result.Succeeded;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var users = repository.GetUsersAsync();

            return await users.ToListAsync();
        }

        public async Task<bool> LogOutAsync()
        {
            await signInManager.SignOutAsync();

            return true;
        }
    }
}
