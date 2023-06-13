namespace CourseApi.Dtos.TeacherDtos
{
    public class TeacherGetDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Subject { get; set; }
        public GroupInTeacherGetDto Group { get; set; }
    }

    public class GroupInTeacherGetDto
    {
        public int Id { get; set; }
        public string No { get; set; }
    }
}

