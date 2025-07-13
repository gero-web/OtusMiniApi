using Application.DTO;
using Application.Interfaces;
using Core;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace OtusMiniApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController(IUserManager user) : ControllerBase
    {
        private readonly IUserManager _user = user;
       
        


        [Authorize]
        [HttpGet(Name = "GetUser")]
        public async Task<User> GetUserProfile()
        {
            var result = await _user.GetUserAsync( );

            return result;
        }


        [HttpPost(Name = "Loggin")]
        public async Task<bool>  LoginUserProfile(string userName, string password)
        { 
            var result = await _user.LoginUserAsync(userName, password);
          
            return result;
        }

        [HttpPost(Name = "RegisterUser")]
        public async Task<bool> RegisterUser(UserDTO user)
        {
            return await _user.CreateUserAsync(user);
        }

        [HttpGet]
        public async Task<bool> LogOut()
        {
            return await _user.LogOutAsync();
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
