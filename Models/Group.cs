namespace CourseApi.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string No { get; set; }

        public List<Student> Students { get; set; } 
    }
}
