using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Order
    {
        [Key]
        public Guid OrderId { get;set; }

        public string UserId;
        public User User { get; set; }

        public string OrderName {get;set;}

        public decimal OrderPrace { get;set;}
    }
}
