using Entities.Dtos;
using Entities.Models;
using Repositories.Contracts;
using StoreApp.Repositories;

namespace Repositories.Concretes
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public IQueryable<Product> GetAllProducts(bool trackChanges) => FindAll(trackChanges);
        public Product GetProductById(int id, bool trackChanges)
        {
            return FindByCondition(p => p.ProductId.Equals(id), trackChanges);
        }

        public void CreateProduct(Product product) => Create(product);
        public void UpdateProduct(Product product) => Update(product);
        public void DeleteProduct(Product product) => Delete(product);
    }
}
