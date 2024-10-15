
using Entities.Models;
using Repositories.Contracts;
using StoreApp.Repositories;

namespace Repositories.Concretes
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public void CreateCategory(Category category) => Create(category);

        public void DeleteCategory(Category category) => Delete(category);

        public Category GetCategoryById(int id, bool trackChanges)
        {
            return FindByCondition(c => c.CategoryId.Equals(id), trackChanges);
        }
    }
}
