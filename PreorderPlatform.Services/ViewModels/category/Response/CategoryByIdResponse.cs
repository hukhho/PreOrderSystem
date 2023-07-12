using PreorderPlatform.Service.ViewModels.Product.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.category.Response
{
    public class CategoryByIdResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<ProductResponse> Products { get; set; }
    }
}
