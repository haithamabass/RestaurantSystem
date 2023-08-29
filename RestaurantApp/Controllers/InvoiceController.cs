using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Dtos.Invoice;
using RestaurantApp.Dtos.Menu;
using RestaurantApp.Models;
using RestaurantApp.Services.Contents.Concretes;
using RestaurantApp.Services.Contents.InterFaces;

namespace RestaurantApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceServices _invoiceServices;
        private readonly IOrderServices _orderService;

        public InvoiceController(IInvoiceServices invoiceServices,  IOrderServices orderService)
        {
            _invoiceServices = invoiceServices;
            _orderService = orderService;
        }



        [HttpGet]
        [Route("GetInvoices")]
        public async Task<ActionResult<IEnumerable<InvoiceDto>>> GetInvoices()
        {
            try
            {

                var invoices = await _invoiceServices.GetAllInvoices();

                if (invoices == null)
                {
                    return NoContent();
                }


                return Ok(invoices);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }



        [HttpGet]
        [Route("GetReadyToPickUpInvoices")]
        public async Task<ActionResult<IEnumerable<InvoiceDto>>> GetReadyToPickUpInvoices()
        {
            try
            {

                var invoices = await _invoiceServices.GetAllInvoicesOfOrdersReadyForPickUp();

                if (invoices == null)
                {
                    return NoContent();
                }


                return Ok(invoices);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }



        [HttpGet]
        [Route("GetReadyForDeliveryInvoices")]
        public async Task<ActionResult<IEnumerable<InvoiceDto>>> GetReadyForDeliveryInvoices()
        {
            try
            {

                var invoices = await _invoiceServices.GetAllInvoicesOfOrdersReadyForDelivery();

                if (invoices == null)
                {
                    return NoContent();
                }


                return Ok(invoices);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }




        [HttpGet]
        [Route("GetInvoiceByInvoiceCode")]
        public async Task<IActionResult> GetInvoiceByInvoiceCode(string invoiceCode)
        {
            try
            {

                var invoices = await _invoiceServices.GetInvoiceByInvoiceCode(invoiceCode);

                if (invoices == null)
                {
                    return NoContent();
                }


                return Ok(invoices);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }



        [HttpGet]
        [Route("GetInvoiceByOrderType")]
        public async Task<IActionResult> GetInvoicesByOrderType(int type)
        {
            try
            {

                var invoices = await _invoiceServices.GetAllInvoicesByOrderType(type);

                if (invoices == null)
                {
                    return NoContent();
                }


                return Ok(invoices);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }



        [HttpPost]
        [Route("CreateInvoice")]

        public async Task<IActionResult> CreateInvoice(string orderCode)
        {
            if (orderCode == null)
            {
                return BadRequest();
            }
            var order = await _orderService.GetOrderByOrderCode(orderCode);


            var invoice = await _invoiceServices.CreateInvoice(order.OrderCode);

            await _orderService.DeleteOrderAsync(order.OrderId);

            return CreatedAtAction(nameof(GetInvoiceByInvoiceCode), new { invoiceCode = invoice.InvoiceCode }, invoice);
        }




        #region Proceed Invoices

        [HttpPut("Proceed Invoices ")]
        public async Task<IActionResult> ProceedInvoices([FromQuery] string code)
        {

            var updatedItem = await _invoiceServices.UpdateInvoicePaymentStatusToPaid(code);

            return Ok(updatedItem);
        }

        #endregion



        [HttpPut("Update Invoices ")]
        public async Task<IActionResult> UpdateInvoice([FromQuery] InvoiceDtoToUpdate invoice)
        {

            var updatedItem = await _invoiceServices.UpdateInvoice(invoice);

            return Ok(updatedItem);
        }



    }
}
