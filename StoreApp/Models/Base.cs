namespace StoreApp.Models
{
    public class Base
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set;}

        public bool isActive { get; set; }
        public bool isDeleted { get; set; }

    }
}
