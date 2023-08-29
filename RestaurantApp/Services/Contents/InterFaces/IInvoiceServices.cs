using RestaurantApp.Dtos.Invoice;
using RestaurantApp.Dtos.Order;
using RestaurantApp.Models;

namespace RestaurantApp.Services.Contents.InterFaces
{
    public interface IInvoiceServices
    {
        Task<List<InvoiceDto>> GetAllInvoices();
        Task<List<InvoiceDto>> GetAllInvoicesByOrderType(int typeId);
        Task<List<InvoiceDto>> GetAllInvoicesOfOrdersReadyForDelivery();
        Task<List<InvoiceDto>> GetAllInvoicesOfOrdersReadyForPickUp();
        Task<InvoiceDto> GetInvoiceByInvoiceCode(string code);
        Task<Invoice> GetInvoiceByInvoiceCodeToUpdate(string code);
        Task<Invoice> AddInvoiceInDatabase(Invoice invoice);
        Invoice UpdateInvoiceInDatabase(Invoice invoice);
        Task<InvoiceDto> UpdateInvoicePaymentStatusToPaid(string code);
        Task<InvoiceDto> UpdateInvoice(InvoiceDtoToUpdate invoice);
        Task<InvoiceDto> CreateInvoice(string orderCode);



    }
}
