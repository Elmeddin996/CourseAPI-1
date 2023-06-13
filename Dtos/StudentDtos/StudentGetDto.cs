namespace CourseApi.Dtos.StudentDtos
{
    public class StudentGetDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int AvgPoint { get; set; }
        public string Email { get; set; }
        public GroupInStudentGetDto Group { get; set; }
    }

    public class GroupInStudentGetDto
    {
        public int Id { get; set; }
        public string No { get; set; }
    }
}
