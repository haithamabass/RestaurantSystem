using RestaurantApp.Models;

namespace RestaurantApp.Helpers.Queue
{
    public interface IOrderQueueService
    {
        Task EnqueueOrder(Order order);
        Task<Order> DequeueOrder();
        Task<OrderQueue> GetOrderQueueAsync(Guid orderId);
        void RemoveOrderQueue(OrderQueue orderQueue);
    }
}
