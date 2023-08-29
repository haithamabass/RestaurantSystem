using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Models
{
    public class OrderQueue
    {
        [Key]
        public int QueuePosition { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

    }
}
