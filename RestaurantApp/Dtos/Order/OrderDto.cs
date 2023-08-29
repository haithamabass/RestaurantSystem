namespace RestaurantApp.Dtos.Order
{
    public class OrderDto
    {
        public OrderWithItemsDto Order { get; set; }
        public OrderItemDto Item { get; set; }
    }
}
