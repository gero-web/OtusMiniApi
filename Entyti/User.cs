namespace Core
{
    public class User
    {
        public long UserId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        
    }
}
