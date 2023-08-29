using Microsoft.EntityFrameworkCore;
using RestaurantApp.Data;
using RestaurantApp.Dtos.Invoice;
using RestaurantApp.Dtos.Order;
using RestaurantApp.Models;
using RestaurantApp.Services.Contents.InterFaces;
using StackExchange.Redis;

namespace RestaurantApp.Services.Contents.Concretes
{
    public class InvoiceServices : IInvoiceServices
    {
        private readonly AppDbContext _context;
        private readonly IOrderServices _orderServices;
        private readonly IPaymentStatus _paymentStatusServices;
        private readonly ILogger<InvoiceServices> _logger;

        public InvoiceServices(AppDbContext context, ILogger<InvoiceServices> logger, IPaymentStatus paymentStatusServices, IOrderServices orderService)
        {
            _context = context;
            _logger = logger;
            _paymentStatusServices = paymentStatusServices;
            _orderServices = orderService;
        }
        #region GetAll
        public async Task<List<InvoiceDto>> GetAllInvoices()
        {
            try
            {
                var invoices = await _context.Invoices
                .Include(oi => oi.OrderItems)
                .ThenInclude(m => m.Menu)
                .Select(i => new InvoiceDto
                {
                    InvoiceDate = i.Date,
                    InvoiceId = i.InvoiceId,
                    InvoiceCode = i.InvoiceCode,
                    OrderCode = i.Order.OrderCode,
                    OrderDate = i.Date,
                    OrderTypeName = i.Order.OrderType.Name,
                    OrderStatusName = i.Order.Status.Name,
                    ReturnCause = i.Order.CancelCause ?? "N/A",
                    ReturnDate = i.Order.CancelDate ?? null,
                    FullName = i.Order.FullName ?? "N/A",
                    PhoneNumber = i.Order.PhoneNumber ?? 0,
                    Address = i.Order.Address ?? "N/A",
                    Total = i.Total,
                    OrderItems = i.OrderItems.Select(oi => new InvoiceItemDto
                    {
                        InvoiceItemId = oi.InvoiceItemId,
                        DishName = oi.Menu.DishName,
                        CategoryName = oi.Menu.Category.Name,
                        Quantity = oi.Quantity,
                        Notes = oi.Notes ?? "No notes",
                        Price = oi.Menu.Price,
                        TotalPriceOfItem = oi.Quantity * oi.Menu.Price
                    }).ToList(),
                    PaymentStatusName = i.PaymentStatus.Name
                }).AsNoTracking()
                .ToListAsync();

                if (invoices is null)
                {
                    throw new Exception("no invoices has found");
                }

                return invoices;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using GetAllInvoices method");

                throw;
            }
        }

        public async Task<List<InvoiceDto>> GetAllInvoicesByOrderType(int typeId)
        {
            try
            {
                var invoices = await _context.Invoices
                 .Where(s => s.Order.OrderTypeId == typeId && s.Order.Status.OrderStatusId != 6)
                 .Include(oi => oi.OrderItems)
                 .ThenInclude(m => m.Menu)
                 .Select(i => new InvoiceDto
                 {
                     InvoiceDate = i.Date,
                     InvoiceId = i.InvoiceId,
                     InvoiceCode = i.InvoiceCode,
                     OrderCode = i.Order.OrderCode,
                     OrderDate = i.Date,
                     OrderTypeName = i.Order.OrderType.Name,
                     OrderStatusName = i.Order.Status.Name,
                     ReturnCause = i.Order.CancelCause ?? "N/A",
                     ReturnDate = i.Order.CancelDate ?? null,
                     FullName = i.Order.FullName ?? "N/A",
                     PhoneNumber = i.Order.PhoneNumber ?? 0,
                     Address = i.Order.Address ?? "N/A",
                     Total = i.Total,
                     OrderItems = i.OrderItems.Select(oi => new InvoiceItemDto
                     {
                         InvoiceItemId = oi.InvoiceItemId,
                         DishName = oi.Menu.DishName,
                         CategoryName = oi.Menu.Category.Name,
                         Quantity = oi.Quantity,
                         Notes = oi.Notes,
                         Price = oi.Menu.Price,
                         TotalPriceOfItem = oi.Quantity * oi.Menu.Price
                     }).ToList(),
                     PaymentStatusName = i.PaymentStatus.Name
                 }).AsNoTracking()
                 .ToListAsync();

                if (invoices is null)
                {
                    throw new Exception("no invoices has found");
                }

                return invoices;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using GetAllInvoicesByOrderType method");

                throw;
            }
        }


        public async Task<List<InvoiceDto>> GetAllInvoicesOfOrdersReadyForDelivery()
        {
            try
            {
                var invoices = await _context.Invoices
                 .Where(s => s.Order.OrderTypeId == 1 && s.Order.Status.OrderStatusId != 6)
                 .Include(oi => oi.OrderItems)
                 .ThenInclude(m => m.Menu)
                 .Select(i => new InvoiceDto
                 {
                     InvoiceDate = i.Date,
                     InvoiceId = i.InvoiceId,
                     InvoiceCode = i.InvoiceCode,
                     OrderCode = i.Order.OrderCode,
                     OrderDate = i.Date,
                     OrderTypeName = i.Order.OrderType.Name,
                     OrderStatusName = i.Order.Status.Name,
                     ReturnCause = i.Order.CancelCause ?? "N/A",
                     ReturnDate = i.Order.CancelDate ?? null,
                     FullName = i.Order.FullName ?? "N/A",
                     PhoneNumber = i.Order.PhoneNumber ?? 0,
                     Address = i.Order.Address ?? "N/A",
                     Total = i.Total,
                     OrderItems = i.OrderItems.Select(oi => new InvoiceItemDto
                     {
                         InvoiceItemId = oi.InvoiceItemId,
                         DishName = oi.Menu.DishName,
                         CategoryName = oi.Menu.Category.Name,
                         Quantity = oi.Quantity,
                         Notes = oi.Notes,
                         Price = oi.Menu.Price,
                         TotalPriceOfItem = oi.Quantity * oi.Menu.Price
                     }).ToList(),
                     PaymentStatusName = i.PaymentStatus.Name
                 }).AsNoTracking()
                 .ToListAsync();

                if (invoices is null)
                {
                    throw new Exception("no invoices has found");
                }

                return invoices;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using GetAllInvoicesOfOrdersReadyForDelivery method");

                throw;
            }
        }

        public async Task<List<InvoiceDto>> GetAllInvoicesOfOrdersReadyForPickUp()
        {
            try
            {
                var invoices = await _context.Invoices
                 .Where(s => s.Order.OrderTypeId == 2 && s.Order.Status.OrderStatusId != 6)
                 .Include(oi => oi.OrderItems)
                 .ThenInclude(m => m.Menu)
                 .Select(i => new InvoiceDto
                 {
                     InvoiceDate = i.Date,
                     InvoiceId = i.InvoiceId,
                     InvoiceCode = i.InvoiceCode,
                     OrderCode = i.Order.OrderCode,
                     OrderDate = i.Date,
                     OrderTypeName = i.Order.OrderType.Name,
                     OrderStatusName = i.Order.Status.Name,
                     ReturnCause = i.Order.CancelCause ?? "N/A",
                     ReturnDate = i.Order.CancelDate ?? null,
                     FullName = i.Order.FullName ?? "N/A",
                     PhoneNumber = i.Order.PhoneNumber ?? 0,
                     Address = i.Order.Address ?? "N/A",
                     Total = i.Total,
                     OrderItems = i.OrderItems.Select(oi => new InvoiceItemDto
                     {
                         InvoiceItemId = oi.InvoiceItemId,
                         DishName = oi.Menu.DishName,
                         CategoryName = oi.Menu.Category.Name,
                         Quantity = oi.Quantity,
                         Notes = oi.Notes,
                         Price = oi.Menu.Price,
                         TotalPriceOfItem = oi.Quantity * oi.Menu.Price
                     }).ToList(),
                     PaymentStatusName = i.PaymentStatus.Name
                 }).AsNoTracking()
                 .ToListAsync();

                if (invoices is null)
                {
                    throw new Exception("no invoices has found");
                }

                return invoices;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using GetAllInvoicesOfOrdersReadyForPickUp method");

                throw;
            }
        }
        #endregion

        #region GetInvoice
        public async Task<InvoiceDto> GetInvoiceByInvoiceCode(string code)
        {
            try
            {
                if (code is null)
                {
                    throw new ArgumentNullException("null input is invalid");
                }

                var invoiceDto = await _context.Invoices
                    .Where(i => i.InvoiceCode == code)
                     .Include(oi => oi.OrderItems)
                     .ThenInclude(m => m.Menu)
                     .ThenInclude(c => c.Category)
                    .Select(i => new InvoiceDto
                    {
                        InvoiceDate = DateTime.Now,
                        InvoiceId = i.InvoiceId,
                        InvoiceCode = i.InvoiceCode,
                        OrderCode = i.Order.OrderCode,
                        OrderDate = i.Date,
                        OrderTypeName = i.Order.OrderType.Name,
                        OrderStatusName = i.Order.Status.Name,
                        ReturnCause = i.Order.CancelCause ?? "N/A",
                        ReturnDate = i.Order.CancelDate ?? null,
                        FullName = i.Order.FullName ?? "N/A",
                        PhoneNumber = i.Order.PhoneNumber ?? 0,
                        Address = i.Order.Address ?? "N/A",
                        Total = i.Total,

                        OrderItems = i.OrderItems.Select(ci => new InvoiceItemDto
                        {
                            InvoiceItemId = ci.InvoiceItemId,
                            DishName = ci.Menu.DishName,
                            CategoryName = ci.Menu.Category.Name,
                            Quantity = ci.Quantity,
                            Notes = ci.Notes ?? "No notes",
                            Price = ci.Menu.Price,
                            TotalPriceOfItem = ci.Quantity * ci.Menu.Price
                        }).ToList(),
                        PaymentStatusName = i.PaymentStatus.Name
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (invoiceDto is null)
                {
                    throw new Exception($"No Invoice was found with this code {invoiceDto}");
                }

                return invoiceDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using GetInvoiceByInvoiceCode method");
                throw;
            }
        }

        public async Task<Invoice> GetInvoiceByInvoiceCodeToUpdate(string code)
        {
            try
            {
                if (code is null)
                {
                    throw new ArgumentNullException("null input is invalid");
                }

                var invoice = await _context.Invoices
                    .Include(i => i.Order)
                    .FirstOrDefaultAsync(i => i.InvoiceCode == code);

                if (invoice is null)
                {
                    throw new Exception($"No Invoice was found with this code {code}");
                }

                return invoice;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while using GetInvoiceByInvoiceCode method");
                throw;
            }
        }
        #endregion

        #region CreateInvoice
        public async Task<InvoiceDto> CreateInvoice(string orderCode)
        {
            try
            {
                if (orderCode is null)
                {
                    throw new ArgumentNullException("null input is invalid");
                }

                var order = await _orderServices.GetOrderByOrderCode(orderCode);

                if (order.Status.OrderStatusId == 1 || order.Status.OrderStatusId == 5)
                {
                    throw new Exception("Cannot create an invoice for unpreceded order");
                }

                Models.Order orderObject = new Models.Order();


                Invoice invoice = new Invoice();

                invoice.Order = orderObject;

                invoice.OrderId = order.OrderId;
                invoice.Date = DateTime.Now;
                invoice.Order.Status = order.Status;
                invoice.Order.OrderTypeId = order.OrderTypeId;
                invoice.Order.OrderCode = order.OrderCode;
                invoice.Order.CancelCause = order.CancelCause;
                invoice.Order.CancelDate = order.CancelDate;
                invoice.Total = order.OrderItems.Sum(ci => ci.Quantity * ci.Menu.Price);
                invoice.Order.FullName = order.FullName ?? "N/A";
                invoice.Order.Address = order.Address ?? "N/A";
                invoice.Order.PhoneNumber = order.PhoneNumber ?? 0;
              
                if (order.Status.OrderStatusId == 6)
                {
                    var paymentStatus = await _paymentStatusServices.GetPaymentStatusById(3);

                    invoice.PaymentStatus = paymentStatus;
                }

                await AddInvoiceInDatabase(invoice);


                invoice.OrderItems = order.OrderItems.Select(ci => new InvoiceItem
                {
                    InvoiceId = invoice.InvoiceId,
                    DishId = ci.Menu.DishId,
                    Quantity = ci.Quantity,
                    Notes = ci.ItemNotes
                }).ToList();

                await _context.SaveChangesAsync();

                var invoiceDto = await GetInvoiceByInvoiceCode(invoice.InvoiceCode);

                return invoiceDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using CreateInvoice method");
                throw;
            }
        }

        public async Task<Invoice> AddInvoiceInDatabase(Invoice invoice)
        {
            try
            {
                _context.Invoices.Add(invoice);
                await _context.SaveChangesAsync();

                return invoice;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using AddInvoiceInDatabase method");
                throw;
            }
        }
        #endregion

        #region UpdateInvoice
        public Invoice UpdateInvoiceInDatabase(Invoice invoice)
        {
            try
            {
                _context.Update(invoice);
                _context.SaveChanges();
                return invoice;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while using UpdateInvoiceInDatabase method");
                throw;
            }
        }
        public async Task<InvoiceDto> UpdateInvoicePaymentStatusToPaid(string code)
        {
            try
            {
                if (code is null)
                {
                    throw new ArgumentNullException("null input is invalid");
                }

                var existInvoice = await GetInvoiceByInvoiceCodeToUpdate(code);

                if (existInvoice.Order.OrderStatusId == 6)
                {
                    //tag the PaymentStatus "Canceled"

                    var paymentStatus = await _paymentStatusServices.GetPaymentStatusById(3);

                    if (paymentStatus is null)
                    {
                        throw new Exception("no status was found");
                    }
                    existInvoice.PaymentStatus = paymentStatus;
                    UpdateInvoiceInDatabase(existInvoice);

                    var existInvoiceDto = await GetInvoiceByInvoiceCode(existInvoice.InvoiceCode);
                    return existInvoiceDto;
                }
                else
                {
                    //tag the PaymentStatus "Paid"

                    var paymentStatus = await _paymentStatusServices.GetPaymentStatusById(2);

                    if (paymentStatus is null)
                    {
                        throw new Exception("no status was found");
                    }
                    existInvoice.PaymentStatus = paymentStatus;
                    UpdateInvoiceInDatabase(existInvoice);

                    var existInvoiceDto = await GetInvoiceByInvoiceCode(existInvoice.InvoiceCode);
                    return existInvoiceDto;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using (UpdateInvoicePaymentStatusToPaid) method");
                throw;
            }
        }
        public async Task<InvoiceDto> UpdateInvoice(InvoiceDtoToUpdate invoice)
        {
            try
            {
                if (invoice is null)
                {
                    throw new ArgumentNullException(nameof(invoice), "The invoice parameter cannot be null.");
                }

                var existInvoice = await GetInvoiceByInvoiceCodeToUpdate(invoice.InvoiceCode);

                if (existInvoice is null)
                {
                    throw new InvalidOperationException($"No invoice found with InvoiceCode {invoice.InvoiceCode}.");
                }


                ValidateInvoicesOrder(invoice);

                if (invoice.OrderStatusId.HasValue)
                    existInvoice.Order.OrderStatusId = invoice.OrderStatusId.Value;


                if (invoice.OrderTypeId.HasValue)
                    existInvoice.Order.OrderTypeId = invoice.OrderTypeId.Value;

                existInvoice.Order.CancelCause = invoice.CancelCause;

                if (invoice.CancelCause is not null)
                    existInvoice.Order.CancelDate = DateTime.Now;

                existInvoice.Order.FullName = invoice.FullName;

                if (invoice.PhoneNumber.HasValue)
                    existInvoice.Order.PhoneNumber = invoice.PhoneNumber.Value;

                existInvoice.Order.Address = invoice.Address;

                UpdateInvoiceInDatabase(existInvoice);

                var existInvoiceDto = await GetInvoiceByInvoiceCode(existInvoice.InvoiceCode);
                return existInvoiceDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using (UpdateInvoice) method");
                throw;
            }
        }

        #endregion

        private void ValidateInvoicesOrder(InvoiceDtoToUpdate invoice)
        {


            if ((invoice.OrderTypeId == 1 || invoice.OrderTypeId == 2) && 
                (string.IsNullOrEmpty(invoice.FullName) || string.IsNullOrEmpty(invoice.Address) || invoice.PhoneNumber == 0))
            {
                throw new ArgumentException("FullName, Address, and PhoneNumber are required for Delivery and Take Away orders");
            }


            if (invoice.OrderStatusId == 6)
            {
                if (string.IsNullOrEmpty(invoice.CancelCause) )
                {
                    throw new ArgumentException("ReturnCause and CancelDate is required to cancel order");
                }

            }



        }

       

    }
}