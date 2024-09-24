
namespace Entities.RequestParameters
{
    public class ProductRequestParameters : RequestParameters
    {
        public int? CategoryId { get; set; }
        public int? minPrice { get; set; } = 0;
        public int? maxPrice { get; set; } = int.MaxValue;
        public string bAction { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public ProductRequestParameters() : this(1,6)
        {
            
        }
        public ProductRequestParameters(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
