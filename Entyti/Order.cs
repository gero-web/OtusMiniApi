namespace Core
{
    public class Order
    {
        public string UserId;
        public User User { get; set; }

        public string OrderName {get;set;}

        public decimal OrderPrace { get;set;}
    }
}
