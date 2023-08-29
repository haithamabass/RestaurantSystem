using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApp.Dtos.Order;
using RestaurantApp.Models;
using RestaurantApp.Services.Contents.Concretes;
using RestaurantApp.Services.Contents.InterFaces;

namespace RestaurantApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderService;
        private readonly IInvoiceServices _invoiceServices;


        public OrderController(IOrderServices orderService, IInvoiceServices invoiceServices)
        {
            _orderService = orderService;
            _invoiceServices = invoiceServices;
        }


        [HttpGet]
        [Route("Receive orders")]
        public async Task<IActionResult> GetOrdersForKitchen()
        {
            try
            {

                var orders = await _orderService.GetAllOrdersForKitchen();

                if (orders == null)
                {
                    return NoContent();
                }


                return Ok(orders);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }  
        
        
        [HttpGet]
        [Route("GetOrders")]
        public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetOrders()
        {
            try
            {

                var cartItems = await _orderService.GetAllOrders();

                if (cartItems == null)
                {
                    return NoContent();
                }


                return Ok(cartItems);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }


        [HttpGet]
        [Route("GetItemsByStatus")]
        public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetOrdersByStatus(int statusId)
        {
            try
            {

                var cartItems = await _orderService.GetAllOrdersByOrderStatus(statusId);

                if (cartItems == null)
                {
                    return NoContent();
                }


                return Ok(cartItems);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }


        [HttpGet]
        [Route("GetItemsByType")]
        public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetOrdersByType(int typeId)
        {
            try
            {

                var cartItems = await _orderService.GetAllOrdersByOrderType(typeId);

                if (cartItems == null)
                {
                    return NoContent();
                }


                return Ok(cartItems);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        [HttpGet]
        [Route("GetOrder")]
        public async Task<ActionResult<OrderDto>> GetOrder(Guid id)
        {
            try
            {
                System.Diagnostics.Debugger.Break();


                var cartItems = await _orderService.GetOrderDto(id);


                System.Diagnostics.Debugger.Break();

                if (cartItems == null)
                {
                    return BadRequest("No Order");
                }


                return Ok(cartItems);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }


        [HttpPost]
        [Route("CreateOrder")]

        public async Task<ActionResult<OrderItemDto>> CreateOrder([FromBody]List<OrderItemToAddDto> cartItemToAddDto)
        {

            try
            {

                var newCartItem = await _orderService.CreateOrder(cartItemToAddDto);

                if (newCartItem == null)
                {
                    return BadRequest("Empty Input ..... !");
                }

                return Ok(newCartItem);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet]
        [Route("GetOrderByOrderCode")]
        public async Task<ActionResult<OrderDto>> GetOrderByOrderCode(string code)
        {
            try
            {
                System.Diagnostics.Debugger.Break();


                var cartItems = await _orderService.GetOrderByOrderCodeDto(code);


                System.Diagnostics.Debugger.Break();

                if (cartItems == null)
                {
                    return BadRequest("No Order");
                }


                return Ok(cartItems);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }


        [HttpPut("Prepare Next Order")]
        public IActionResult PrepareNextOrder()
        {
            var order = _orderService.PrepareNextOrder();
            if (order is null)
            {
                return NotFound("No Orders");
            }
            return Ok(order);
        }

        [HttpPut("Serve")]
        public async Task <IActionResult> ServeNextOrder(string orderCode)
        {
            var order = await _orderService.ServeOrder(orderCode);
            if (order is null)
            {
                return NotFound("No Orders");
            }

           var invoice = await _invoiceServices.CreateInvoice(order.OrderCode);

           await _orderService.DeleteOrderAsync(order.OrderId);
            return Ok(invoice);
        }

        
     
        [HttpPut("cancel order")]
        public async Task<IActionResult> CancelOrder([FromQuery] CancelOrderDto order)
        {
            try
            {

                var canceledOrder = await _orderService.UpdateOrderStatusToCancelOrder(order);



               var invoice =  await _invoiceServices.CreateInvoice(canceledOrder.OrderCode);

                await _orderService.DeleteOrderAsync(canceledOrder.OrderId);

                return Ok(invoice);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
