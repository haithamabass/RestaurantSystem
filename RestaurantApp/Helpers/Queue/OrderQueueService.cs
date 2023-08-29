using Microsoft.EntityFrameworkCore;
using RestaurantApp.Data;
using RestaurantApp.Models;

namespace RestaurantApp.Helpers.Queue
{
    public class OrderQueueService:IOrderQueueService
    {
        private readonly AppDbContext _context;

        public OrderQueueService(AppDbContext context)
        {
            _context = context;
        }

        public async Task EnqueueOrder(Order order)
        {
            // Find the next available position in the queue
            int nextPosition = await _context.OrderQueue.MaxAsync(o => (int?)o.QueuePosition) + 1 ?? 1;


            // Add the order to the queue
            _context.OrderQueue.Add(new OrderQueue { OrderId = order.OrderId });
            await _context.SaveChangesAsync();
        }

        public async Task<Order> DequeueOrder()
        {
            // Find the first order in the queue
            var firstOrderInQueue = await _context.OrderQueue.OrderBy(o => o.QueuePosition).FirstOrDefaultAsync();

            if (firstOrderInQueue is null)
            {
               
                throw new ArgumentException("Invalid firstOrderInQueue (via DequeueOrder) ");
            }
          
            // Remove the order from the queue
            _context.OrderQueue.Remove(firstOrderInQueue);
            await _context.SaveChangesAsync();

            // Return the order
            return await _context.Orders.Include(o => o.OrderType).FirstOrDefaultAsync(o => o.OrderId == firstOrderInQueue.OrderId);
        }


        public async Task<OrderQueue> GetOrderQueueAsync(Guid orderId)
        {
            var orderQueue = await _context.OrderQueue
                .FirstOrDefaultAsync(oq => oq.OrderId == orderId);
            return orderQueue;
        }

        public void RemoveOrderQueue(OrderQueue orderQueue)
        {
            _context.OrderQueue.Remove(orderQueue);
        }

    }

}
