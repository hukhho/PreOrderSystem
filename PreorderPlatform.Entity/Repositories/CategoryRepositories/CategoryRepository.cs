using PreorderPlatform.Entity.Models;

namespace PreorderPlatform.Entity.Repositories.CategoryRepositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(PreOrderSystemContext context) : base(context)
        {

        }
    }
}