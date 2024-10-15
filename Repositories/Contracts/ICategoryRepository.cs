using Entities.Models;

namespace Repositories.Contracts
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        void CreateCategory(Category category);
        void DeleteCategory(Category category);
        Category GetCategoryById(int id, bool trackChanges);
        
    }
}
