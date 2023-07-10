using PreorderPlatform.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Entity.Repositories.ProductRepositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(PreOrderSystemContext context) : base(context)
        {

        }

        // Add any additional methods specific to ProductRepository here...
        public async Task<IEnumerable<Product>> GetAllProductsWithCategoryAsync()
        {
            return await GetAllWithIncludeAsync(u => true, u => u.Category);
        }
    }
}