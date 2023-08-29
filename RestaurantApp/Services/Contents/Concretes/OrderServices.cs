using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestaurantApp.Data;
using RestaurantApp.Dtos;
using RestaurantApp.Dtos.Order;
using RestaurantApp.Dtos.OrderQueue;
using RestaurantApp.Helpers.Queue;
using RestaurantApp.Models;
using RestaurantApp.Services.Contents.InterFaces;
using System.Security.Claims;
using System.Web.Helpers;

namespace RestaurantApp.Services.Contents.Concretes
{
    public class OrderServices:IOrderServices
    {
        private readonly AppDbContext _context;
        private readonly IOrderQueueService _orderQueueService;

        private readonly ILogger<OrderServices> _logger;

        public OrderServices(AppDbContext context, ILogger<OrderServices> logger, IOrderQueueService orderQueueService)
        {
            _context = context;
            _logger = logger;
            _orderQueueService = orderQueueService;
        }


        #region GetAllOrders

        public async Task<List<OrderWithItemsDto>> GetAllOrders()
        {
            try
            {
                var orders = await (from Order in _context.Orders
                                    .OrderByDescending(o => o.OrderDate)
                                    join OrderItem in _context.OrderItems
                                         on Order.OrderId equals OrderItem.OrderId
                                    join Menu in _context.DishesAndOthers
                                         on OrderItem.DishId equals Menu.DishId
                                    join OrderStatus in _context.OrderStatuses
                                         on Order.OrderStatusId equals OrderStatus.OrderStatusId
                                    join OrderType in _context.OrderTypes
                                         on Order.OrderTypeId equals OrderType.OrderTypeId
                                    select new OrderDto
                                    {
                                        Order = new OrderWithItemsDto
                                        {

                                            OrderId = Order.OrderId,
                                            OrderDate = Order.OrderDate,
                                            OrderCode = Order.OrderCode,
                                            OrderStatusId = Order.OrderStatusId,
                                            OrderStatusName = OrderStatus.Name,
                                            CancelCause = OrderStatus.OrderStatusId == 6 ? Order.CancelCause : "N/A",
                                            CancelDate = OrderStatus.OrderStatusId == 6 ? Order.CancelDate : null,
                                            OrderTypeId = Order.OrderTypeId,
                                            OrderTypeName = OrderType.Name,
                                            FullName = OrderType.OrderTypeId != 3 ? Order.FullName : "N/A",
                                            PhoneNumber = OrderType.OrderTypeId != 3 ? Order.PhoneNumber : null,
                                            Address = OrderType.OrderTypeId != 3 ? Order.Address : "N/A",
                                        },
                                        Item = new OrderItemDto
                                        {
                                            ItemIdDto = OrderItem.ItemId,
                                            DishId = OrderItem.DishId,
                                            DishName = Menu.DishName,
                                            Description = Menu.Description,
                                            Price = Menu.Price,
                                            Quantity = OrderItem.Quantity,
                                            TotalPrice = Menu.Price * OrderItem.Quantity,
                                            ItemNotes = Order.Notes ?? "No notes"
                                        }

                                    }).AsNoTracking().ToListAsync();

                var ordersWithItems = orders.GroupBy(i => new { i.Order.OrderId, i.Order.OrderStatusId, i.Order.OrderCode })
                                              .Select(g => new OrderWithItemsDto
                                              {
                                                  OrderId = g.Key.OrderId,
                                                  OrderCode = g.First().Order.OrderCode,
                                                  Items = g.Select(i => i.Item).ToList(),
                                                  OrderStatusId = g.First().Order.OrderStatusId,
                                                  OrderStatusName = g.First().Order.OrderStatusName,
                                                  CancelCause = g.First().Order.OrderStatusId == 6 ? g.First().Order.CancelCause : "N/A",
                                                  CancelDate = g.First().Order.OrderStatusId == 6 ? g.First().Order.CancelDate : null,
                                                  OrderTypeId = g.First().Order.OrderTypeId,
                                                  OrderTypeName = g.First().Order.OrderTypeName,
                                                  OrderDate = g.First().Order.OrderDate,
                                                  FullName = g.First().Order.OrderTypeId != 3 ? g.First().Order.FullName : "N/A",
                                                  PhoneNumber = g.First().Order.OrderTypeId != 3 ? g.First().Order.PhoneNumber : null,
                                                  Address = g.First().Order.OrderTypeId != 3 ? g.First().Order.Address : "N/A",

                                              }).ToList();

                return ordersWithItems;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using GetAllOrders method");
                throw;
            }

           

        } 
        
        public async Task<List<OrderQueueDto>> GetAllOrdersForKitchen()
        {

            try
            {
                var ordersInQueue = await _context.OrderQueue
                .OrderBy(oq => oq.QueuePosition)
                .Select(oq => new OrderQueueDto
                {
                    QueuePosition = oq.QueuePosition,
                    OrderId = oq.OrderId,
                    OrderCode = oq.Order.OrderCode,
                    OrderDate = oq.Order.OrderDate,
                    OrderItems = oq.Order.OrderItems.Select(oi => new OrderQueueItemDto
                    {
                        ItemId = oi.ItemId,
                        DishName = oi.Menu.DishName,
                        Quantity = oi.Quantity,
                        Notes = oi.ItemNotes ?? "No notes"
                    }).ToList()
                })
                .AsNoTracking()
                .ToListAsync();

                return ordersInQueue;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using GetAllOrdersForKitchen method");
                throw;
            }


        }

        public async Task<List<OrderWithItemsDto>> GetAllOrdersByOrderStatus(int statusId)
        {

            try
            {
                var items = await (from OrderItem in _context.OrderItems
                                   join Order in _context.Orders
                                         .Where(o => o.Status.OrderStatusId == statusId)
                                        .OrderByDescending(o => o.OrderDate)
                                        on OrderItem.OrderId equals Order.OrderId
                                   join Menu in _context.DishesAndOthers on OrderItem.DishId equals Menu.DishId
                                   join OrderStatus in _context.OrderStatuses on Order.OrderStatusId equals OrderStatus.OrderStatusId
                                   join OrderType in _context.OrderTypes on Order.OrderTypeId equals OrderType.OrderTypeId
                                   select new OrderDto
                                   {
                                       Order = new OrderWithItemsDto
                                       {

                                           OrderId = Order.OrderId,
                                           OrderCode = Order.OrderCode,
                                           OrderStatusId = Order.OrderStatusId,
                                           OrderStatusName = OrderStatus.Name,
                                           OrderTypeId = Order.OrderTypeId,
                                           OrderTypeName = OrderType.Name
                                       },
                                       Item = new OrderItemDto
                                       {
                                           ItemIdDto = OrderItem.ItemId,
                                           DishId = OrderItem.DishId,
                                           DishName = Menu.DishName,
                                           Description = Menu.Description,
                                           
                                           Price = Menu.Price,
                                           Quantity = OrderItem.Quantity,
                                           TotalPrice = Menu.Price * OrderItem.Quantity,
                                           ItemNotes = Order.Notes
                                       }

                                   }).AsNoTracking().ToListAsync();

                var ordersWithItems = items.GroupBy(i => new { i.Order.OrderId, i.Order.OrderStatusId })
                                              .Select(g => new OrderWithItemsDto
                                              {
                                                  OrderId = g.Key.OrderId,
                                                  OrderCode = g.First().Order.OrderCode,
                                                  Items = g.Select(i => i.Item).ToList(),
                                                  OrderStatusId = g.First().Order.OrderStatusId,
                                                  OrderStatusName = g.First().Order.OrderStatusName,
                                                  OrderTypeId = g.First().Order.OrderTypeId,
                                                  OrderTypeName = g.First().Order.OrderTypeName
                                              }).ToList();



                return ordersWithItems;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using GetAllOrdersByOrderStatus method");
                throw;
            }


         


        }
        public async Task<List<OrderWithItemsDto>> GetAllOrdersByOrderType(int typeId)
        {

            try
            {
                var items = await (from OrderItem in _context.OrderItems
                                   join Order in _context.Orders
                                         .Where(t => t.OrderTypeId == typeId)
                                         .OrderByDescending(o => o.OrderDate)
                                         on OrderItem.OrderId equals Order.OrderId
                                   join Menu in _context.DishesAndOthers on OrderItem.DishId equals Menu.DishId
                                   join OrderStatus in _context.OrderStatuses on Order.OrderStatusId equals OrderStatus.OrderStatusId
                                   join OrderType in _context.OrderTypes on Order.OrderTypeId equals OrderType.OrderTypeId
                                   select new OrderDto
                                   {
                                       Order = new OrderWithItemsDto
                                       {
                                           OrderId = Order.OrderId,
                                           OrderCode = Order.OrderCode,
                                           OrderStatusId = Order.OrderStatusId,
                                           OrderStatusName = OrderStatus.Name,
                                           OrderTypeId = Order.OrderTypeId,
                                           OrderTypeName = OrderType.Name
                                       },
                                       Item = new OrderItemDto
                                       {
                                           ItemIdDto = OrderItem.ItemId,
                                           DishId = OrderItem.DishId,
                                           DishName = Menu.DishName,
                                           Description = Menu.Description,
                                           Price = Menu.Price,
                                           Quantity = OrderItem.Quantity,
                                           TotalPrice = Menu.Price * OrderItem.Quantity,
                                           ItemNotes = Order.Notes
                                       }

                                   }).AsNoTracking().ToListAsync();

                var ordersWithItems = items.GroupBy(i => new { i.Order.OrderId, i.Order.OrderStatusId })
                                              .Select(g => new OrderWithItemsDto
                                              {
                                                  OrderId = g.Key.OrderId,
                                                  OrderCode = g.First().Order.OrderCode,
                                                  Items = g.Select(i => i.Item).ToList(),
                                                  OrderStatusId = g.First().Order.OrderStatusId,
                                                  OrderStatusName = g.First().Order.OrderStatusName,
                                                  OrderTypeId = g.First().Order.OrderTypeId,
                                                  OrderTypeName = g.First().Order.OrderTypeName
                                              }).ToList();

                return ordersWithItems;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using GetAllOrdersByOrderType method");
                throw;
            }

        }

        #endregion

        #region GetOrder

        public async Task<OrderQueueDto> GetOrderForKitchen(Guid id)
        {

            try
            {
                var orderInQueue = await _context.Orders
                    .Where(oq=> oq.OrderId == id)
                .Select(oq => new OrderQueueDto
                {
                    OrderId = oq.OrderId,
                    OrderCode = oq.OrderCode,
                    OrderDate = oq.OrderDate,
                    OrderItems = oq.OrderItems.Select(oi => new OrderQueueItemDto
                    {
                        ItemId = oi.ItemId,
                        DishName = oi.Menu.DishName,
                        Quantity = oi.Quantity,
                        Notes = oi.ItemNotes ?? "No notes"
                    }).ToList()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();

                if (orderInQueue is null)
                    throw new Exception("No Order Found");

                return orderInQueue;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using GetOrderForKitchen method");
                throw;
            }

           
        }

        public async Task<Order> GetOrder(Guid orderid)
        {
            try
            {

                var order = await _context.Orders
                    .Include(o => o.OrderType)
                    .Include(o => o.Status)
                    .Where(o => o.OrderId == orderid)
                    .FirstOrDefaultAsync();

                if (order is null)
                    throw new Exception("No Order Found");

                return order;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using GetOrder method");
                throw;
            }



        }
        public async Task<OrderWithItemsDto> GetOrderByOrderCodeDto(string orderCode)
        {

            try
            {
                var order = await (from o in _context.Orders
                                   join ot in _context.OrderTypes on o.OrderTypeId equals ot.OrderTypeId
                                   join os in _context.OrderStatuses on o.OrderStatusId equals os.OrderStatusId
                                   where o.OrderCode == orderCode
                                   select new OrderWithItemsDto
                                   {
                                       OrderId = o.OrderId,
                                       OrderDate = o.OrderDate,
                                       OrderCode = o.OrderCode,
                                       OrderStatusId = os.OrderStatusId,
                                       OrderStatusName = os.Name,
                                       CancelCause = os.OrderStatusId == 6 ? o.CancelCause : "N/A",
                                       CancelDate = os.OrderStatusId == 6 ? o.CancelDate : null,
                                       OrderTypeId = ot.OrderTypeId,
                                       OrderTypeName = ot.Name,
                                       FullName = ot.OrderTypeId != 3 ? o.FullName : "N/A",
                                       PhoneNumber = ot.OrderTypeId != 3 ? o.PhoneNumber : null,
                                       Address = ot.OrderTypeId != 3 ? o.Address : "N/A",
                                       Items = (from i in _context.OrderItems
                                                where i.OrderId == o.OrderId
                                                join m in _context.DishesAndOthers on i.DishId equals m.DishId
                                                select new OrderItemDto
                                                {
                                                    ItemIdDto = i.ItemId,
                                                    DishId = i.DishId,
                                                    DishName = m.DishName,
                                                    Description = m.Description,
                                                    Price = m.Price,
                                                    Quantity = i.Quantity,
                                                    TotalPrice = m.Price * i.Quantity,
                                                    ItemNotes = i.ItemNotes ?? "No notes"
                                                }).ToList()
                                   })
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync();

                if (order is null)
                    throw new Exception("No Order Found");

                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using GetOrderByOrderCodeDto method");
                throw;
            }

            


        }

        public async Task<Order> GetOrderByOrderCode(string orderCode)
        {
            try
            {
                if (orderCode is null)
                {
                    throw new ArgumentNullException("null input is invalid");
                }

                var order = await _context.Orders
                    .Include(o => o.OrderType)
                    .Include(o => o.Status)
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Menu)
                    .Where(o => o.OrderCode == orderCode)
                    .FirstOrDefaultAsync();

                if (order is null)
                {
                    throw new Exception($"No order was found with this code {orderCode}");
                }

                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using GetOrderByOrderCode method");
                throw;
            }
        }


        public async Task<OrderWithItemsDto> GetOrderDto(Guid orderid)
        {
            try
            {

                var order = await (from o in _context.Orders
                                   join ot in _context.OrderTypes on o.OrderTypeId equals ot.OrderTypeId
                                   join os in _context.OrderStatuses on o.OrderStatusId equals os.OrderStatusId
                                   where o.OrderId == orderid
                                   select new OrderWithItemsDto
                                   {
                                       OrderId = o.OrderId,
                                       OrderCode = o.OrderCode,
                                       OrderStatusId = os.OrderStatusId,
                                       OrderStatusName = os.Name,
                                       OrderTypeId = ot.OrderTypeId,
                                       OrderTypeName = ot.Name,
                                       Items = (from i in _context.OrderItems
                                                join m in _context.DishesAndOthers on i.DishId equals m.DishId
                                                where i.OrderId == o.OrderId
                                                select new OrderItemDto
                                                {
                                                    ItemIdDto = i.ItemId,
                                                    DishId = i.DishId,
                                                    DishName = m.DishName,
                                                    Description = m.Description,
                                                    Price = m.Price,
                                                    Quantity = i.Quantity,
                                                    TotalPrice = m.Price * i.Quantity,
                                                    ItemNotes = i.ItemNotes
                                                }).ToList()
                                   })
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync();

                if (order is null)
                {
                    throw new Exception("No Orders Found");
                }


                return order;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using GetOrderDto method");
                throw;
            }
        }

        #endregion

        #region Proceed Orders
        public async Task<List<OrderItem>> CreateOrder(List<OrderItemToAddDto> orderItemsToAdd)
        {
            try
            {
                if (orderItemsToAdd.Count == 0)
                {
                    throw new Exception("The order Items list must not be empty");
                }

                var status = await _context.OrderStatuses.FirstOrDefaultAsync(s => s.OrderStatusId == 5);

                if (status is null)
                {
                    throw new Exception("Invalid OrderStatusId");
                }

                ValidateOrderByType(orderItemsToAdd);

                var orderType = await _context.OrderTypes.FindAsync(orderItemsToAdd[0].OrderTypeId);

                if (orderType is null)
                {
                    throw new Exception("Invalid OrderTypeId");
                }

                 var order = new Order
                {
                    OrderDate = DateTime.Now,
                    Status = status,
                    OrderTypeId = orderType.OrderTypeId,
                    Notes = orderItemsToAdd[0].ItemNotes,
                    FullName = orderItemsToAdd[0].FullName,
                    PhoneNumber = orderItemsToAdd[0].PhoneNumber,
                    Address = orderItemsToAdd[0].Address,

                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                await _orderQueueService.EnqueueOrder(order);


                var orderItems = new List<OrderItem>();

                foreach (var item in orderItemsToAdd)
                {
                    var dish = await _context.DishesAndOthers.FindAsync(item.DishId);

                    if (dish is null)
                    {
                        throw new Exception("Invalid DishID ");
                    }

                    var Item = new OrderItem
                    {
                        DishId = item.DishId,
                        Quantity = item.Quantity,
                        ItemNotes = item.ItemNotes,
                        OrderId = order.OrderId
                    };


                    _context.OrderItems.Add(Item);
                    orderItems.Add(Item);
                }

                await _context.SaveChangesAsync();

                return orderItems;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using Create Order method");
                throw;
            }
            
        }

        public async Task <OrderQueueDto> PrepareNextOrder() 
        {
            try
            {

                var order = await _orderQueueService.DequeueOrder();

                if (order is null)
                {
                    throw new Exception("Empty order "); 
                }

                if (order.Status.OrderStatusId != 5)
                {
                    throw new Exception("Cannot prepare an prepared order already");
                }

                await UpdateOrderStatusToPrepare(order);

                UpdateOrderInDatabase(order);

                var orderdto = await GetOrderForKitchen(order.OrderId);

                return orderdto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using PrepareNextOrder method");
                throw;
            }
        }

        public async Task <Order> ServeOrder(string orderCode)
        {
            try
            {
                if (orderCode is null)
                {
                    throw new ArgumentNullException("Invalid Input ");
                }

                var order = await GetOrderByOrderCode(orderCode);

                if (order is null)
                {
                    throw new Exception("Empty order "); 
                }

                if (order.Status.OrderStatusId != 1)
                {
                    throw new Exception("Cannot serve unprepared order");
                }

                UpdateFinishedOrderStatus(order);
                UpdateOrderInDatabase(order);


                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using 'ServeOrder' method");
                throw;
            }
        }

        #endregion

        #region Update orders
        public void UpdateFinishedOrderStatus(Order order)
        {
            try
            {
                if (order.OrderType is null)
                {
                    throw new Exception("No OrderType was found ");
                }

                switch (order.OrderType.OrderTypeId)
                {
                    case 1: // Delivery
                        var readyForDelivery = _context.OrderStatuses.FirstOrDefault(s => s.OrderStatusId == 4);

                        if (readyForDelivery is null)
                        {
                            throw new Exception($"No status was found with this name");
                        }

                        order.Status = readyForDelivery;

                        break;
                    case 2: // Take Away

                        var readyToPickup = _context.OrderStatuses.FirstOrDefault(s => s.OrderStatusId == 3);

                        if (readyToPickup is null)
                        {
                            throw new Exception($"No status was found with this name");
                        }

                        order.Status = readyToPickup;

                        break;
                    case 3: // On Site
                        var readyToBeServedInHouse = _context.OrderStatuses.FirstOrDefault(s => s.OrderStatusId == 2);

                        if (readyToBeServedInHouse is null)
                        {
                            throw new Exception($"No status was found with this name");
                        }
                        order.Status = readyToBeServedInHouse;
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using PrepareNextOrder method");
                throw;
            }
        }

        public async Task UpdateOrderStatusToDone(Order order)
        {

            try
            {
                if (order.Status == null)
                {
                    throw new ArgumentNullException("order has no status");
                }
                var orderStatus = await _context.OrderStatuses.FirstOrDefaultAsync(o => o.OrderStatusId == 7);

                if (orderStatus is null)
                {
                    throw new Exception("no status was found");
                }
                order.Status = orderStatus;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using (UpdateOrderStatusToDone) method");
                throw;
            }
        }
        public async Task UpdateOrderStatusToPrepare(Order order)
        {

            try
            {
                if (order.Status == null)
                {
                    throw new Exception("Err: Order without status");
                }
                var orderStatus = await _context.OrderStatuses.FirstOrDefaultAsync(o => o.OrderStatusId == 1);
                if (orderStatus is null)
                {
                    throw new Exception("no status was found");
                }
                order.Status = orderStatus;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using (UpdateOrderStatusToPrepare) method");
                throw;
            }
        }

        public async Task<Order> UpdateOrderStatusToCancelOrder(CancelOrderDto order)
        {

            try
            {
                var existOrder = await GetOrderByOrderCode(order.OrderCode);
                if (existOrder is null)
                {
                    throw new Exception($"No order with {order.OrderCode} was found");
                }
                var orderStatus = await _context.OrderStatuses.FirstOrDefaultAsync(o => o.OrderStatusId == 6);

                ValidateCancelOrder(order);

                if (orderStatus is null)
                {
                    throw new Exception("no status has found");
                }
                existOrder.Status = orderStatus;
                existOrder.CancelDate = DateTime.Now;
                existOrder.CancelCause = order.ReturnCause;

                UpdateOrderInDatabase(existOrder);

                return existOrder;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using (UpdateOrderStatusToCancelOrder) method");
                throw;
            }
        }

        public Order UpdateOrderInDatabase(Order order)
        {
            _context.Update(order);
            _context.SaveChanges();
            return order;
        }

        #endregion

        #region Delete Order

        public async Task DeleteOrderAsync (Guid orderId)
        {
            try
            {
                if (orderId == null)
                {
                    throw new ArgumentNullException("Invalid Input ");
                }

                var order = await GetOrder(orderId);

                if (order is null)
                {
                    throw new Exception($"No order was found with this Id {orderId} to be deleted");
                }

                var orderQueue = await _context.OrderQueue
                    .FirstOrDefaultAsync(oq => oq.OrderId == orderId);

                if (orderQueue is  null)
                {

                    _context.Orders.Remove(order);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _context.OrderQueue.Remove(orderQueue);
                    _context.Orders.Remove(order);
                    await _context.SaveChangesAsync();

                }

                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using DeleteOrderAsync method");
                throw;
            }

        }

        #endregion


        private void ValidateOrderByType(List<OrderItemToAddDto> orderItemsToAdd)
        {
            if ((orderItemsToAdd[0].OrderTypeId == 1 || orderItemsToAdd[0].OrderTypeId == 2) && 
                (string.IsNullOrEmpty(orderItemsToAdd[0].FullName) || string.IsNullOrEmpty(orderItemsToAdd[0].Address) || orderItemsToAdd[0].PhoneNumber == 0))
            {
                throw new Exception("FullName, Address, and PhoneNumber are required for Delivery and Take Away orders");
            }


        }
        private void ValidateCancelOrder(CancelOrderDto order)
        {
            if (string.IsNullOrEmpty(order.ReturnCause))
            {
                throw new Exception("ReturnCause is required to cancel order");
            }

        }




    }
}
