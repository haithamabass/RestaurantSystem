namespace RestaurantApp.Dtos.Invoice
{
    public class InvoiceDtoToUpdate
    {
        public string InvoiceCode { get; set; }
        public int? OrderStatusId { get; set; }
        public string? CancelCause { get; set; }
        public int? OrderTypeId { get; set; }
        public string? FullName { get; set; }
        public int? PhoneNumber { get; set; }
        public string? Address { get; set; }
       
    }
}
