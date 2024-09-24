using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

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

        public static IQueryable<Product> FilteredByPrice(this IQueryable<Product> products, int? minPrice, int? maxPrice, string? bAction)
        {
            minPrice ??= 0;
            maxPrice ??= int.MaxValue;

            if (bAction == "filter" && maxPrice>minPrice)
                return products.Where(prd => prd.Price >= minPrice && prd.Price <= maxPrice);
            else
                return products;
        }
    
        public static IQueryable<Product> ToPaginate(this IQueryable<Product> products, int pageNumber,int pageSize) 
        {
            return products
                .Skip(((pageNumber - 1) * pageSize))
                .Take(pageSize);
        }
    }
}
