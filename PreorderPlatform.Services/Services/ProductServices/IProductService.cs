using PreorderPlatform.Service.ViewModels.Product.Request;
using PreorderPlatform.Service.ViewModels.Product.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.Services.ProductServices
{
    public interface IProductService
    {
        Task<ProductResponse> CreateProductAsync(ProductCreateRequest model);
        Task DeleteProductAsync(int id);
        Task<List<ProductResponse>> GetAllProductsWithCategoryAsync();
        Task<ProductResponse> GetProductByIdAsync(int id);
        Task<List<ProductResponse>> GetProductsAsync();
        Task UpdateProductAsync(ProductUpdateRequest model);
    }
}
