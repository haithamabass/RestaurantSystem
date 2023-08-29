//using Foolproof;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantApp.Models
{
    public class Order
    {
        //a default constructor
        public Order()
        {
            OrderId = Guid.NewGuid();
            OrderCode = GenerateOrderNum();
            OrderItems = new List<OrderItem>();
            Status = new OrderStatus { OrderStatusId = 5, Name = "Pending" };

        }




        public Guid OrderId { get; set; }
        public string OrderCode { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }
        public int OrderTypeId { get; set; } 
        public int OrderStatusId { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public OrderType OrderType { get; set; }
        public OrderStatus Status { get; set; }

        public string? FullName { get; set; }

        public int? PhoneNumber { get; set; }

        public string? Address { get; set; }


        public DateTime? CancelDate { get; set; }

        public string? CancelCause { get; set; }






        private string GenerateOrderNum()
        {
            var chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var random = new Random();
            var result = new char[8];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = chars[random.Next(chars.Length)];
            }
            return "OC-" + new string(result);
        }


      





    }
}
