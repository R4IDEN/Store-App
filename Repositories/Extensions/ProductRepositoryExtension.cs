using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Extensions
{
    public static class ProductRepositoryExtension
    {
        public static IQueryable<Product> FilteredByCategoryId(this IQueryable<Product> products, int? CategoryId)
        {
            if (CategoryId is null)
                return products;
            else
                return products.Where(prd => prd.CategoryId == CategoryId);
        }

        public static IQueryable<Product> FilteredBySearchTerm(this IQueryable<Product> products, string? searchTerm)
        {
            if(string.IsNullOrWhiteSpace(searchTerm))
                return products;
            else
                return products.Where(prd=> prd.Name.ToLower().Contains(searchTerm.ToLower()));
        }

        public static IQueryable<Product> FilteredByPrice(this IQueryable<Product> products, int? minPrice, int? maxPrice, bool IsValidPrice)
        {
            if (IsValidPrice)
                return products.Where(prd => prd.Price >= minPrice && prd.Price <= maxPrice);
            else
                return products;
        }
    }
}
