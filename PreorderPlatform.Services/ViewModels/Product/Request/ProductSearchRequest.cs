using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.Product.Request
{
    public class ProductSearchRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? Status { get; set; }
        public int? CategoryId { get; set; }
        public int? BusinessId { get; set; }
    }
}
