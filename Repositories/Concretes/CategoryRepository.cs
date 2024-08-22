
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
    }
}
