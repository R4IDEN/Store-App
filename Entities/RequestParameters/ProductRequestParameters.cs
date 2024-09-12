
namespace Entities.RequestParameters
{
    public class ProductRequestParameters : RequestParameters
    {
        public int? CategoryId { get; set; }
        public int? minPrice { get; set; } = 0;
        public int? maxPrice { get; set; } = int.MaxValue;
        public bool IsValidPrice => maxPrice > minPrice;
    }
}
