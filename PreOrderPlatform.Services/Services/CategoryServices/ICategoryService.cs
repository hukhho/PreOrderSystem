using PreOrderPlatform.Service.Enums;
using PreOrderPlatform.Service.Utility.Pagination;
using PreOrderPlatform.Service.ViewModels.Category;

namespace PreOrderPlatform.Service.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<CategoryViewModel> CreateCategoryAsync(CategoryCreateViewModel model);
        Task DeleteCategoryAsync(Guid categoryId);
        Task<List<CategoryViewModel>> GetCategoriesAsync();
        Task<CategoryViewModel> GetCategoryByIdAsync(Guid categoryId);
        Task UpdateCategoryAsync(CategoryUpdateViewModel model);
        Task<(IList<CategoryViewModel> categories, int totalItems)> GetAsync(PaginationParam<CategoryEnum.CategorySort> paginationModel, CategorySearchRequest filterModel);
        
    }
}
