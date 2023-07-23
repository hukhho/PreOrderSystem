using PreOrderPlatform.Entity.Models;

namespace PreOrderPlatform.Entity.Repositories.PaymentRepositories
{
    public interface IPaymentRepository : IRepositoryBase<Payment>
    {
        Task<bool> IsUserCanAccessPayment(Guid userId, Guid paymentId);
    }
}
