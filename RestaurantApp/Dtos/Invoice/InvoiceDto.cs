namespace RestaurantApp.Dtos.Invoice
{
    public class InvoiceDto
    {
        public DateTime InvoiceDate { get; set; }
        public Guid InvoiceId { get; set; }
        public string InvoiceCode { get; set; }
        public string OrderCode { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderTypeName { get; set; }
        public string OrderStatusName { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string? ReturnCause { get; set; }
        public string? FullName { get; set; }
        public int? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public List<InvoiceItemDto> OrderItems { get; set; }
        public decimal Total { get; set; }
        public string  PaymentStatusName { get; set; }
    }
}
