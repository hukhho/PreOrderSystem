using Microsoft.EntityFrameworkCore;
using PreOrderPlatform.Entity.Models;

namespace PreOrderPlatform.Entity.Repositories.PaymentRepositories
{
    public class PaymentRepository : RepositoryBase<Payment>, IPaymentRepository
    {
        private readonly PreOrderSystemContext _context;

        public PaymentRepository(PreOrderSystemContext context) : base(context)
        {
            _context = context;
        }



        public async Task<bool> IsUserCanAccessPayment(Guid userId, Guid paymentId)
        {
            // Check if the user is the owner of the payment
            bool isUserPaymentOwner = await _context.Payments.AnyAsync(p => p.Id == paymentId && p.UserId == userId);

            if (isUserPaymentOwner)
            {
                return true;
            }

            // Check if the user is related to the order of the payment
            bool canAccessPayment = await _context.Orders
                .Include(o => o.Payments)
                .ThenInclude(p => p.User)
                .AnyAsync(o => o.Payments.Any(p => p.Id == paymentId) && (o.User.Id == userId || o.User.Business.Users.Any(u => u.Id == userId)));

            return canAccessPayment;
        }


        // Add any additional methods specific to PaymentRepository here...
    }
}