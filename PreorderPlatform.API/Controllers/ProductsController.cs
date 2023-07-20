using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PreorderPlatform.Service.Services.ProductServices;
using PreorderPlatform.Service.ViewModels.ApiResponse;
using PreorderPlatform.Service.Services.Exceptions;
using PreorderPlatform.Service.Exceptions;
using PreorderPlatform.Service.ViewModels.Product.Request;
using PreorderPlatform.Service.ViewModels.Product.Response;
using PreorderPlatform.Service.Utility.Pagination;
using PreorderPlatform.Services.Enum;
using Microsoft.AspNetCore.Authorization;
using PreorderPlatform.Service.Utility.CustomAuthorizeAttribute;
using System.Security.Claims;
using PreorderPlatform.Service.Services.UserServices;

namespace PreorderPlatform.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        public ProductsController(IProductService productService, IUserService userService)
        {
            _productService = productService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts(
            [FromQuery] PaginationParam<ProductEnum.ProductSort> paginationModel,
            [FromQuery] ProductSearchRequest searchModel
        )
        {
            try
            {
                var start = DateTime.Now;
                var (products, totalItems) = await _productService.GetAsync(paginationModel, searchModel);
                Console.Write(DateTime.Now.Subtract(start).Milliseconds);

                return Ok(new ApiResponse<IList<ProductResponse>>(
                    products,
                    "Products fetched successfully.",
                    true,
                    new PaginationInfo(totalItems, paginationModel.PageSize, paginationModel.Page, (int)Math.Ceiling(totalItems / (double)paginationModel.PageSize))
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error fetching products: {ex.Message}", false, null));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                return Ok(new ApiResponse<ProductByIdResponse>(product, "Product fetched successfully.", true, null));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<string>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error fetching product: {ex.Message}", false, null));
            }
        }

        [HttpPost]
        [CustomAuthorize(Roles = "ADMIN,BUSSINESS_OWNER,BUSINESS_STAFF")]
        public async Task<IActionResult> CreateProduct(ProductCreateRequest model)
        {
            try
            {
                var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var user = await _userService.GetUserByIdAsync(userId);

                if (user.BusinessId == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                                      new ApiResponse<object>(null, "Error creating product", false, null));
                }

                model.BusinessId = (Guid) user.BusinessId;

                var product = await _productService.CreateProductAsync(model);
                return CreatedAtAction(nameof(GetProductById),
                                       new { id = product.Id },
                                       new ApiResponse<ProductResponse>(product, "Product created successfully.", true, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error creating product: {ex.Message}", false, null));
            }
        }

        [HttpPut]
        [Authorize(Policy = "MustBeProductAccess")]
        public async Task<IActionResult> UpdateProduct(ProductUpdateRequest model)
        {
            try
            {
                await _productService.UpdateProductAsync(model);
                return Ok(new ApiResponse<object>(null, "Product updated successfully.", true, null));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<object>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error updating product: {ex.Message}", false, null));
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "MustBeProductAccess")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                return Ok(new ApiResponse<object>(null, "Product deleted successfully.", true, null));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<string>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error deleting product: {ex.Message}", false, null));
            }
        }
    }
}