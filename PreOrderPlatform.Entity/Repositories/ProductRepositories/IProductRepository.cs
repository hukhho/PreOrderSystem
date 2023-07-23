using PreOrderPlatform.Entity.Models;

namespace PreOrderPlatform.Entity.Repositories.ProductRepositories
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<IEnumerable<Product>> GetAllProductsWithCategoryAsync();
        Task<Product> GetProductByIdAsync(Guid id);
        Task<bool> IsUserCanAccessProduct(Guid userId, Guid productId);
    }
}
