using PreorderPlatform.Service.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<CategoryViewModel> CreateCategoryAsync(CategoryCreateViewModel model);
        Task DeleteCategoryAsync(int id);
        Task<List<CategoryViewModel>> GetCategoriesAsync();
        Task<CategoryViewModel> GetCategoryByIdAsync(int id);
        Task UpdateCategoryAsync(CategoryUpdateViewModel model);
    }
}
