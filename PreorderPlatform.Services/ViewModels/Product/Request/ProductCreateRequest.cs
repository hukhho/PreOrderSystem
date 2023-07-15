﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.Product.Request
{
    public class ProductCreateRequest
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public bool? Status { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? BusinessId { get; set; }
    }
}
