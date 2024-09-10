
using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    public record ProductDTO
    {
        public int ProductId { get; init; }
        [Required(ErrorMessage = "Product Name is required.")]
        public string Name { get; init; }
        [Required(ErrorMessage = "Price is mandatory.")]
        public decimal Price { get; set; }
        public string? ImageURL { get; set; }
        public string? Summary { get; set; }
        public bool Showcase { get; set; }
        public int? CategoryId { get; set; }
    }
}