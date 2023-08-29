using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Dtos.Order
{
    public class OrderItemToAddDto
    {

        public Guid DishId { get; set; }
        public int Quantity { get; set; }
        public string? ItemNotes { get; set; }
        public int OrderTypeId { get; set; }
        public string? FullName { get; set; }

        public int? PhoneNumber { get; set; }

        public string? Address { get; set; }

    }
}
