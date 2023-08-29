using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantApp.Models
{
    public class DishImage
    {
        [Key]
        public int Id { get; set; }
        public byte[] Image { get; set; }

        public Guid DishId { get; set; }
        [ForeignKey("DishId")]
        public Menu Menu { get; set; }
    }
}
