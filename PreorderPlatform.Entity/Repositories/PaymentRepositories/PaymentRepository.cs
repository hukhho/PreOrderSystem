using Microsoft.EntityFrameworkCore;
using PreorderPlatform.Entity.Models;

namespace PreorderPlatform.Entity.Repositories.PaymentRepositories
{
    public class PaymentRepository : RepositoryBase<Payment>, IPaymentRepository
    {
        public PaymentRepository(PreOrderSystemContext context) : base(context)
        {

        }

        // Add any additional methods specific to PaymentRepository here...
        public async Task<Payment> GetPaymentByIdAsync(int id)
        {
            var payments = await GetWithIncludeAsync(
                p => p.Id == id,
                p => p.Include(u => u.User).ThenInclude(r => r.Role),
                p => p.Include(o => o.Order)
                );
            return payments;
        }
    }
}