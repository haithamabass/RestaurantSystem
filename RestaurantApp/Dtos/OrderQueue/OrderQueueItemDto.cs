namespace RestaurantApp.Dtos.OrderQueue
{
    public class OrderQueueItemDto
    {
        public Guid ItemId { get; set; }
        public string DishName { get; set; }
        public int Quantity { get; set; }
        public string Notes { get; set; }

    }
}
