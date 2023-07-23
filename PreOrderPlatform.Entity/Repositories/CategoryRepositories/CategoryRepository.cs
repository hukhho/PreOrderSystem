using PreOrderPlatform.Entity.Models;

namespace PreOrderPlatform.Entity.Repositories.CategoryRepositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(PreOrderSystemContext context) : base(context)
        {

        }
    }
}