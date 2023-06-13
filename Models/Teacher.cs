namespace CourseApi.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Subject { get; set; }   
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
