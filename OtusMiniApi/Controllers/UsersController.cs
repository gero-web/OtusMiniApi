using Application.DTO;
using Application.Interfaces;
using Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OtusMiniApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUser user) : ControllerBase
    {
        private IUser _user { get; } = user;

        [HttpGet(Name = "GetUser")]
        public async Task<User> GetUser(long userId)
        {
            var result = await _user.GetUserAsync(userId);

            return result;
        } 

        [HttpPost(Name = "CreateUser")]
        public async Task<bool> CreateUser( UserDTO user)
        {
            var result = await _user.CreateUserAsync(user);

            return result;
        }



        [HttpPut(Name = "EditUser")]
        public async Task<User> EditUser( User user)
        {
            var result = await _user.EditUserAsync(user);

            return result;
        }
    }
}
