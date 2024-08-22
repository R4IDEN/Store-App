using System.ComponentModel.DataAnnotations;

namespace StoreApp.Models
{
    public class Candidate : Base
    {
        public Candidate() 
        {
            isActive = true;
            isDeleted = false;
            CreatedDate = DateTime.Now;
        }

        [Required(ErrorMessage ="Name is required")]
        public string FullName{ get; set; }
        [Required(ErrorMessage = "E-mail is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Age is required")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Please select a course.")]
        public string SelectedCourse{ get; set; }
    }
}
