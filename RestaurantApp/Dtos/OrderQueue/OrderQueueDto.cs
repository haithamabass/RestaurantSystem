namespace RestaurantApp.Dtos.OrderQueue
{
    public class OrderQueueDto
    {
        public int QueuePosition { get; set; }
        public Guid OrderId { get; set; }
        public string OrderCode { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderQueueItemDto> OrderItems { get; set; }
    }
}
