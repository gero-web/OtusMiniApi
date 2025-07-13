using Microsoft.AspNetCore.Identity;

namespace Core
{
    public class User : IdentityUser
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }

    }
}
