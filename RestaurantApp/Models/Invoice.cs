using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Foolproof;

namespace RestaurantApp.Models
{
    public class Invoice 
    {


        public Invoice()
        {
            InvoiceId = Guid.NewGuid();
            InvoiceCode = GenerateOrderNum();
            PaymentStatus = new PaymentStatus {Name = "Unpaid yet" };
        }



        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid InvoiceId { get; set; }
        public string InvoiceCode { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public List<InvoiceItem> OrderItems { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        [ForeignKey("PaymentStatusId")]
        public int PaymentStatusId { get; set; }

        public PaymentStatus PaymentStatus { get; set; } 











        private string GenerateOrderNum()
        {
            var chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var random = new Random();
            var result = new char[8];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = chars[random.Next(chars.Length)];
            }
            return "InvC-" + new string(result);
        }






    }
}
