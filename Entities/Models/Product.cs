using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Product : Base
    {
        public Product()
        {
            isActive = true;
            isDeleted = false;
            CreatedDate = DateTime.Now;
        }

        public int ProductId { get; init; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Summary { get; set; }
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; } //Foreign Key
        public Category? Category { get; set; } //Navigation Property
    }
}
