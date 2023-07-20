using PreorderPlatform.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Entity.Repositories.PaymentRepositories
{
    public interface IPaymentRepository : IRepositoryBase<Payment>
    {
        Task<bool> IsUserCanAccessPayment(Guid userId, Guid paymentId);
    }
}
