using Microsoft.EntityFrameworkCore;
using PreOrderPlatform.Entity.Models;

namespace PreOrderPlatform.Entity.Repositories.ProductRepositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private readonly PreOrderSystemContext _context;
        public ProductRepository(PreOrderSystemContext context) : base(context)
        {
            _context = context;
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
        public async Task<bool> IsUserCanAccessPayment(Guid userId, Guid paymentId)
        {
            // Check if the user is the owner of the payment
            bool isUserPaymentOwner = await _context.Payments.AnyAsync(p => p.Id == paymentId && p.UserId == userId);

            if (isUserPaymentOwner)
            {
                return true;
            }

            // Check if the user is related to the order of the payment
            bool canAccessPayment = await _context.Orders
                .Include(o => o.Payments)
                .ThenInclude(p => p.User)
                .AnyAsync(o => o.Payments.Any(p => p.Id == paymentId) && (o.User.Id == userId || o.User.Business.Users.Any(u => u.Id == userId)));

            return canAccessPayment;
        }

        public async Task<bool> IsUserCanAccessProduct(Guid userId, Guid productId)
        {
              // Check if the user is the owner of the product
            bool isUserProductOwner = await _context.Products.AnyAsync(p => p.Id == productId && p.Business.OwnerId == userId);

            if (isUserProductOwner)
            {
                return true;
            }

            // Check if the user is a staff member of the business
            bool canAccessProduct = await _context.Products
                .Include(p => p.Business)
                .ThenInclude(b => b.Users)
                .AnyAsync(p => p.Id == productId && p.Business.Users.Any(u => u.Id == userId));

            return canAccessProduct;
        }
       


    }
}