using Entities.Models;
using Entities.RequestParameters;
using Repositories.Contracts;
using Repositories.Extensions;
using StoreApp.Repositories;

namespace Repositories.Concretes
{
    //sealed : bu sinif ile kalitim yapamayacaginizi gosterir. 
    public sealed class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public IQueryable<Product> GetAllProducts(bool trackChanges) => FindAll(trackChanges);
        public IQueryable<Product> GetAllProductsWithDetails(ProductRequestParameters p)
        {
            //
            return _context.Products
                .FilteredByCategoryId(p.CategoryId)
                .FilteredBySearchTerm(p.SearchTerm)
                .FilteredByPrice(p.minPrice, p.maxPrice, p.bAction)
                .ToPaginate(p.PageNumber,p.PageSize);
        }
        public Product GetProductById(int id, bool trackChanges)
        {
            return FindByCondition(p => p.ProductId.Equals(id), trackChanges);
        }
        public IQueryable<Product> GetShowcaseProducts(bool trackChanges)
        {
            return FindAll(trackChanges).Where(p => p.Showcase.Equals(true));
        }

        public void CreateProduct(Product product) => Create(product);
        public void UpdateProduct(Product product) => Update(product);
        public void DeleteProduct(Product product) => Delete(product);

    }
}
