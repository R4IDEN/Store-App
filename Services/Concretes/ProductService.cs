using AutoMapper;
using Entities.Dtos;
using Entities.Models;
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
        public IEnumerable<Product> GetAllProducts(bool trackChanges)
        {
            return _repositoryManager.Product.GetAllProducts(trackChanges);
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
    }
}
