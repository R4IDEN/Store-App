
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Order:Base
    {
        public Order()
        {
            isActive = true;
            isDeleted = false;
            CreatedDate = DateTime.Now;
        }
        public int OrderId { get; set; }
        public ICollection<CartLine> Lines { get; set; } = new List<CartLine>();


        [Required(ErrorMessage ="Name is mandatory.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is mandatory.")]
        public string? Line1 { get; set; }
        public string? Line2 { get; set; }
        public string City { get; set; }
        public bool isGift { get; set; }
        public bool isShipped { get; set; }

    }
}
