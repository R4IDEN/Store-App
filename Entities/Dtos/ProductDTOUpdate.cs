
namespace Entities.Dtos
{
    public record ProductDTOUpdate : ProductDTO
    {
        public DateTime UpdatedDate { get; set; }
    }
}
