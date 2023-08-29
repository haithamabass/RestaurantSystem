using RestaurantApp.Dtos.Order;
using RestaurantApp.Dtos.OrderQueue;
using RestaurantApp.Models;

namespace RestaurantApp.Services.Contents.InterFaces
{
    public interface IOrderServices
    {
        Task<List<OrderQueueDto>> GetAllOrdersForKitchen();
        Task<List<OrderWithItemsDto>> GetAllOrders();
        Task<List<OrderWithItemsDto>> GetAllOrdersByOrderStatus(int statusId);
        Task<List<OrderWithItemsDto>> GetAllOrdersByOrderType(int typeId);

        Task<Order> GetOrder(Guid orderid);
        Task<OrderWithItemsDto> GetOrderByOrderCodeDto(string orderCode);
        Task<Order> GetOrderByOrderCode(string orderCode);
        Task<OrderWithItemsDto> GetOrderDto(Guid orderid);

        Task<OrderQueueDto> GetOrderForKitchen(Guid id);
        Task<List<OrderItem>> CreateOrder(List<OrderItemToAddDto> orderItemsToAdd);
        Task<OrderQueueDto> PrepareNextOrder();
        Task<Order> ServeOrder(string orderCode);

        void UpdateFinishedOrderStatus(Order order);
        Task<Order>UpdateOrderStatusToCancelOrder(CancelOrderDto order);
        Task UpdateOrderStatusToPrepare(Order order);
        Task UpdateOrderStatusToDone(Order order);
        Order UpdateOrderInDatabase(Order order);


        Task DeleteOrderAsync(Guid orderId);


    }
}
