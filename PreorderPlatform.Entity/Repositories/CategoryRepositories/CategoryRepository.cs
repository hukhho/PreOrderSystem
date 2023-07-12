using Microsoft.EntityFrameworkCore;
using PreorderPlatform.Entity.Models;

namespace PreorderPlatform.Entity.Repositories.CategoryRepositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(PreOrderSystemContext context) : base(context)
        {

        }
        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var categories = await GetWithIncludeAsync(
                c => c.Id == id,
                c => c.Include(p => p.Products)
                );
            return categories;
        }
    }
}