using Entities.Models;

namespace Repositories.Contracts
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        void CreateCategory(Category category);
    }
}
