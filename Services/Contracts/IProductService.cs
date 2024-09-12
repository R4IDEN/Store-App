using Entities.Dtos;
using Entities.Models;
using Entities.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts(bool trackChanges);
        IEnumerable<Product> GetAllProductsWithDetails(ProductRequestParameters p);
        IEnumerable<Product> GetShowcaseProducts(bool trackChanges);

        Product? GetProductWithId(int id, bool trackChanges);
        ProductDTOUpdate? GetProductWithIdForUpdate(int id, bool trackChanges);
        void CreateProduct(ProductDTOInsertion productCreateDTO);
        void UpdateProduct(ProductDTOUpdate productUpdateDTO);
        bool DeleteProduct(int id);
    }
}
