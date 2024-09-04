using Repositories.Contracts;
using StoreApp.Repositories;

namespace Repositories.Concretes
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly AppDbContext _appDbContext;

        public RepositoryManager(IProductRepository productRepository, AppDbContext appDbContext, ICategoryRepository categoryRepository, IOrderRepository orderRepository)
        {
            _appDbContext = appDbContext;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _orderRepository = orderRepository;
        }

        public IProductRepository Product => _productRepository;
        public ICategoryRepository Category => _categoryRepository;
        public IOrderRepository Order => _orderRepository;


        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}
