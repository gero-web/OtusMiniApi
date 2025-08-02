using Application.DTO;
using Application.Interfaces;
using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OtusMiniApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController(IUserManager user, ILogger<UsersController> logger) : ControllerBase
    {
        private  readonly ILogger<UsersController> _logger = logger;
        private readonly IUserManager _user = user;
       
        [Authorize]
        [HttpGet(Name = "GetUser")]
        public async Task<User> GetUserProfile()
        {
            _logger.LogWarning($"User is Auth {User?.Identity?.IsAuthenticated}");
            var result =await _user.GetUserAsync();
            return result;
        }

   
        [Authorize]
        [HttpPut(Name = "EditUser")]
        public async Task<User> EditUser(UserDTO user)
        { 
            var result = await _user.EditUserAsync(user);

            return result;
        }
    }
}
