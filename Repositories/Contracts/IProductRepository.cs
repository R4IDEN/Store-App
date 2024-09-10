using Entities.Dtos;
using Entities.Models;

namespace Repositories.Contracts
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        //Task<List<Product>> GetAllProducts();
        IQueryable<Product> GetAllProducts(bool trackChanges);
        IQueryable<Product> GetShowcaseProducts(bool trackChanges);

        Product GetProductById(int id, bool trackChanges);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
    }
}
