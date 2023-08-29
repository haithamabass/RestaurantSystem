using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantApp.Models
{
    public class InvoiceItem
    {
        [Key]
        public Guid InvoiceItemId { get; set; }
        public Guid InvoiceId { get; set; }

        [ForeignKey("Menu")]
        public Guid DishId { get; set; }
        public int Quantity { get; set; }

        public string? Notes { get; set; }

        public Menu Menu { get; set; }
        public Invoice Invoice { get; set; }

    }
}
