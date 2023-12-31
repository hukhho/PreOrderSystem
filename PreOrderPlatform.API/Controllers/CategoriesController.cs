﻿using Microsoft.AspNetCore.Mvc;
using PreOrderPlatform.Service.Enums;
using PreOrderPlatform.Service.Services.CategoryServices;
using PreOrderPlatform.Service.Services.Exceptions;
using PreOrderPlatform.Service.Utility.CustomAuthorizeAttribute;
using PreOrderPlatform.Service.Utility.Pagination;
using PreOrderPlatform.Service.ViewModels.ApiResponse;
using PreOrderPlatform.Service.ViewModels.Category;

namespace PreOrderPlatform.API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories(
            [FromQuery] PaginationParam<CategoryEnum.CategorySort> paginationModel,
            [FromQuery] CategorySearchRequest searchModel
        )
        {
            try
            {
                var start = DateTime.Now;
                var (categories, totalItems) = await _categoryService.GetAsync(paginationModel, searchModel);
                Console.Write(DateTime.Now.Subtract(start).Milliseconds);

                return Ok(new ApiResponse<IList<CategoryViewModel>>(
                    categories,
                    "Categories fetched successfully.",
                    true,
                    new PaginationInfo(totalItems, paginationModel.PageSize, paginationModel.Page, (int)Math.Ceiling(totalItems / (double)paginationModel.PageSize))
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error fetching categories: {ex.Message}", false, null));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);
                return Ok(new ApiResponse<CategoryViewModel>(category, "Category fetched successfully.", true, null));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<string>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error fetching category: {ex.Message}", false, null));
            }
        }

        [HttpPost]
        [CustomAuthorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateCategory(CategoryCreateViewModel model)
        {
            try
            {
                var category = await _categoryService.CreateCategoryAsync(model);
                return CreatedAtAction(nameof(GetCategoryById),
                                       new { id = category.Id },
                                       new ApiResponse<CategoryViewModel>(category, "Category created successfully.", true, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error creating category: {ex.Message}", false, null));
            }
        }

        [HttpPut]
        [CustomAuthorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateCategory(CategoryUpdateViewModel model)
        {
            try
            {
                await _categoryService.UpdateCategoryAsync(model);
                return Ok(new ApiResponse<object>(null, "Category updated successfully.", true, null));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<object>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error updating category: {ex.Message}", false, null));
            }
        }

        [HttpDelete("{id}")]
        [CustomAuthorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(id);
                return Ok(new ApiResponse<object>(null, "Category deleted successfully.", true, null));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<string>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error deleting category: {ex.Message}", false, null));
            }
        }
    }
}