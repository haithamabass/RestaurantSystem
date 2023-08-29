namespace RestaurantApp.Dtos.Order
{
    public class OrderItemDto
    {
        public Guid ItemIdDto { get; set; }
        public Guid DishId { get; set; }
        public string DishName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? ItemNotes { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
