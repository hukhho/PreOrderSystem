using PreOrderPlatform.Service.Enums;
using PreOrderPlatform.Service.Utility.Pagination;
using PreOrderPlatform.Service.ViewModels.Product.Request;
using PreOrderPlatform.Service.ViewModels.Product.Response;

namespace PreOrderPlatform.Service.Services.ProductServices
{
    public interface IProductService
    {
        Task<ProductResponse> CreateProductAsync(ProductCreateRequest model);
        Task DeleteProductAsync(Guid productId);
        Task<List<ProductResponse>> GetAllProductsWithCategoryAsync();
        Task<ProductByIdResponse> GetProductByIdAsync(Guid productId);
        Task<List<ProductResponse>> GetProductsAsync();
        Task UpdateProductAsync(ProductUpdateRequest model);
        Task<(IList<ProductResponse> products, int totalItems)> GetAsync(PaginationParam<ProductEnum.ProductSort> paginationModel, ProductSearchRequest filterModel);
        
    }
}
