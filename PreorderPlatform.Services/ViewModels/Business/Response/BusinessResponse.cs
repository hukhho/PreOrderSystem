using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.Business.Response
{
    public class BusinessResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public Guid? OwnerId { get; set; }
        public bool? Status { get; set; }

    }
}
