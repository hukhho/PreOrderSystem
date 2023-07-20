﻿using PreorderPlatform.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Entity.Repositories.ProductRepositories
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<IEnumerable<Product>> GetAllProductsWithCategoryAsync();
        Task<Product> GetProductByIdAsync(Guid id);
        Task<bool> IsUserCanAccessProduct(Guid userId, Guid productId);
    }
}
