using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Category:Base
    {
        public Category()
        {
            isActive = true;
            isDeleted = false;
            CreatedDate = DateTime.Now;
        }
        public int CategoryId{ get; set; }
        public string Name { get; set; }
        public ICollection<Product>? Products { get; set; } //Collection navigation property
    }
}
