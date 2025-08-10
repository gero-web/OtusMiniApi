 namespace Core
{
    public class Notifications
    {
        public Guid Id { get; set; } 

        public string Notification { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
