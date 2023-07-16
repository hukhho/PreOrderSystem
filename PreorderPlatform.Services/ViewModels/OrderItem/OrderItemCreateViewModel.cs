using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.OrderItem
{
    public class OrderItemCreateViewModel
    {

        //public Guid? CampaignDetailId { get; set; }
        [Range(1, int.MaxValue)]
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
