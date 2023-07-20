using PreorderPlatform.Entity.Repositories.Enum.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.Payment
{
    public class PaymentCreateViewModel
    {
        public Guid Id { get; set; }
        //[]
        //public int? PaymentCount { get; set; }
        public decimal? PaymentAmount { get; set; }
        public PaymentMethod Method { get; set; }
        //public Guid? UserId { get; set; }
        public Guid? OrderId { get; set; }
    }
}
