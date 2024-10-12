using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Entities.RequestParameters;
using Repositories.Contracts;
using Services.Contracts;
using System.Net.Http.Headers;

namespace Services.Concretes
{
    public class ProductService : IProductService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ProductService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }
        
        public void CreateProduct(ProductDTOInsertion productCreateDTO)
        {
            var product = _mapper.Map<Product>(productCreateDTO);

            _repositoryManager.Product.CreateProduct(product);
            _repositoryManager.Save();
        }
        public void UpdateProduct(ProductDTOUpdate productUpdateDTO)
        {
            var product = _mapper.Map<Product>(productUpdateDTO);

            _repositoryManager.Product.UpdateProduct(product);
            _repositoryManager.Save();
        }
        public bool DeleteProduct(int id)
        {
            var model = _repositoryManager.Product.GetProductById(id, false);
            if(model != null) 
            {
                _repositoryManager.Product.DeleteProduct(model);
                _repositoryManager.Save();
                return true;
            }
            return false;
        }
        public void ToggleShowcase(int id)
        {
            var product = GetProductWithId(id, false);

            if (product is null)
                throw new Exception("Showcase update process failed, try again.");

            product.Showcase = !product.Showcase;

            _repositoryManager.Product.UpdateProduct(product);
            _repositoryManager.Save();
            return;
        }


        public IEnumerable<Product> GetAllProducts(bool trackChanges)
        {
            return _repositoryManager.Product.GetAllProducts(trackChanges);
        }
        public IEnumerable<Product> GetAllProductsWithDetails(ProductRequestParameters p)
        {
            return _repositoryManager.Product.GetAllProductsWithDetails(p);
        }
        public IEnumerable<Product> GetShowcaseProducts(bool trackChanges)
        {
            return _repositoryManager.Product.GetShowcaseProducts(trackChanges);
        }
        public Product? GetProductWithId(int id, bool trackChanges)
        {
            var product = _repositoryManager.Product.GetProductById(id, trackChanges);
            return product == null ? throw new Exception("Product not found") : product;
        }
        public ProductDTOUpdate? GetProductWithIdForUpdate(int id, bool trackChanges)
        {
            var product = _repositoryManager.Product.GetProductById(id, trackChanges);
            var productDTO = _mapper.Map<ProductDTOUpdate>(product);
            return product == null ? throw new Exception("Product not found") : productDTO;
        }
        public IEnumerable<Product> GetLatestProducts(int n, bool trackChanges)
        {
            return _repositoryManager
                .Product
                .FindAll(trackChanges)
                .OrderByDescending(prd => prd.ProductId)
                .Take(n);
        }
    }
}
