using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.Order.Request
{
    public class OrderSearchRequest
    {
        public DateTime? StartDateInRange { get; set; }
        public DateTime? EndDateInRange { get; set; }
        public string? Status { get; set; }
        public string? ReceiverName { get; set; }
        public string? ReceiverPhone { get; set; }
        public int? UserId { get; set; }
    }
}
