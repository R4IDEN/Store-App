namespace StoreApp.Models
{
    public class Course : Base
    {
        public Course() 
        {
            isActive = true;
            isDeleted = false;
            CreatedDate = DateTime.Now;
        }

        public string Name{ get; set; }

        public List<Candidate>? CourseList { get; set; }
    }
}
