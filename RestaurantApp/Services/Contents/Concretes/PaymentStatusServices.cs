using Microsoft.EntityFrameworkCore;
using RestaurantApp.Data;
using RestaurantApp.Dtos.Category;
using RestaurantApp.Models;
using RestaurantApp.Services.Contents.InterFaces;

namespace RestaurantApp.Services.Contents.Concretes
{
    public class PaymentStatusServices : IPaymentStatus
    {
        private readonly AppDbContext _context;

        private readonly ILogger<PaymentStatusServices> _logger;

        public PaymentStatusServices(AppDbContext context, ILogger<PaymentStatusServices> logger)
        {
            _context = context;
            _logger = logger;
        }

        #region GetAllPaymentStatuses
        public async Task<List<PaymentStatus>> GetAllPaymentStatuses()
        {
            try
            {
                return await _context.PaymentStatuses
                      .AsNoTracking()
                    .ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using (GetAll) method CategoryServices");
                throw;
            }
        }
        #endregion

        #region GetPaymentStatusById
        public async Task<PaymentStatus> GetPaymentStatusById(int id)
        {

            try
            {
                if (id == null)
                {
                    throw new ArgumentNullException("null input is invalid");
                }

                var paymentStatus = await _context.PaymentStatuses.FirstOrDefaultAsync(ps => ps.PaymentStatusId == id);

                if (paymentStatus is null)
                {
                    throw new Exception("no paymentStatus was found");
                }

                return paymentStatus;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "NO PRODUCT HAS FOUND (via GetPaymentStatusById )");
                throw;
            }


        }

        #endregion

        #region AddPaymentStatus
        public async Task<PaymentStatus> AddPaymentStatus(PaymentStatus model)
        {
            try
            {

                await _context.PaymentStatuses.AddAsync(model);

                _context.SaveChanges();

                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using (AddPaymentStatus) method");
                throw;
            }
        }
        #endregion

        #region UpdatePaymentStatus
        public async Task<PaymentStatus> UpdatePaymentStatus(int id, PaymentStatus model)
        {
            try
            {
                var existPaymentStatus = await GetPaymentStatusById(id);

                existPaymentStatus.Name = model.Name;

                _context.Update(existPaymentStatus);
                _context.SaveChanges();
                return existPaymentStatus;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using (UpdatePaymentStatus)");
                throw;
            }
        }
        #endregion

        #region DeletePaymentStatus
        public PaymentStatus DeletePaymentStatus(PaymentStatus model)
        {
            try
            {
                _context.Remove(model);
                _context.SaveChanges();
                return model;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using (DeletePaymentStatus)");
                throw;
            }
        }
        #endregion

        #region PaymentStatusIsExist
        public async Task<bool> PaymentStatusIsExist(string paymentStatusName)
        {
            try
            {
                return await _context.PaymentStatuses.AnyAsync(p => p.Name == paymentStatusName);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using (PaymentStatusIsExist)");
                throw;
            }
        }
        #endregion


    }
}
