using PreorderPlatform.Entity.Models;

namespace PreorderPlatform.Entity.Repositories.PaymentRepositories
{
    public class PaymentRepository : RepositoryBase<Payment>, IPaymentRepository
    {
        public PaymentRepository(PreOrderSystemContext context) : base(context)
        {

        }

        // Add any additional methods specific to PaymentRepository here...
    }
}