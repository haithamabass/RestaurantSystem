using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Models
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid ItemId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive integer")]
        public int Quantity { get; set; }

        public string? ItemNotes { get; set; }

        public Guid DishId { get; set; }
        public Guid OrderId { get; set; }


        [ForeignKey("DishId")]
        public  Menu Menu { get; set; }
    }
}
