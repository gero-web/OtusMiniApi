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
             var result =await _user.GetUserAsync();
            return result;
        }


        [HttpGet(Name = "Append")]
        public async Task<IActionResult> AppendBalans([FromQuery] decimal many)
        {
            await _user.AppendBalanse(many);

            return Ok();
        }

        [HttpGet(Name = "SpendBalance")]
        public async Task<IActionResult> SpendBalance([FromQuery] decimal many)
        {
            await _user.SpendBalance(many);

            return Ok();
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
