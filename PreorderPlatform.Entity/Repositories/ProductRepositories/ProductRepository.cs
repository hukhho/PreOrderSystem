using Microsoft.EntityFrameworkCore;
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

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            var product = await GetWithIncludeAsync(
             p => p.Id == id,
             p => p.Include(b => b.Business),
             p => p.Include(b => b.Category),
             p => p.Include(b => b.Campaigns)
             );

            return product;
        }
    }
}