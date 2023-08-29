namespace RestaurantApp.Dtos.Invoice
{
    public class InvoiceItemDto
    {

        public Guid InvoiceItemId { get; set; }
        public string DishName { get; set; }
        public string CategoryName { get; set; }
        public int Quantity { get; set; }
        public string? Notes { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPriceOfItem { get; set; }
    }
}
