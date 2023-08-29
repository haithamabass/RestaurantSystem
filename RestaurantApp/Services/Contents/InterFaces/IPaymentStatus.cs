using RestaurantApp.Models;

namespace RestaurantApp.Services.Contents.InterFaces
{
    public interface IPaymentStatus
    {
        Task<List<PaymentStatus>> GetAllPaymentStatuses();
        Task<PaymentStatus> GetPaymentStatusById(int id);
        Task<PaymentStatus> AddPaymentStatus(PaymentStatus model);
        Task<PaymentStatus> UpdatePaymentStatus(int id, PaymentStatus model);
        PaymentStatus DeletePaymentStatus(PaymentStatus model);
        Task<bool> PaymentStatusIsExist(string paymentStatusName);

    }
}
