namespace Entities.Models
{
    public class Base
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public bool isActive { get; set; }
        public bool isDeleted { get; set; }

    }
}
