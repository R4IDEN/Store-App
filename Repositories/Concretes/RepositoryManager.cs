using Repositories.Contracts;
using StoreApp.Repositories;

namespace Repositories.Concretes
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly AppDbContext _appDbContext;

        public RepositoryManager(IProductRepository productRepository, AppDbContext appDbContext, ICategoryRepository categoryRepository)
        {
            _appDbContext = appDbContext;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public IProductRepository Product => _productRepository;

        public ICategoryRepository Category => _categoryRepository;

        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}
