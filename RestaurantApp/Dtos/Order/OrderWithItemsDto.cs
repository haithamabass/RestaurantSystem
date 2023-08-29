namespace RestaurantApp.Dtos.Order
{
    public class OrderWithItemsDto
    {

        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }

        public string OrderCode { get; set; }
        public int OrderStatusId { get; set; }
        public string OrderStatusName { get; set; }
        public DateTime? CancelDate { get; set; }
        public string? CancelCause { get; set; }
        public int OrderTypeId { get; set; }
        public string OrderTypeName { get; set; }

        public string? FullName { get; set; }

        public int? PhoneNumber { get; set; }

        public string? Address { get; set; }


        public List<OrderItemDto> Items { get; set; }
    }

}
