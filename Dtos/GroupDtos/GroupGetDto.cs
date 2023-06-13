namespace CourseApi.Dtos.GroupDtos
{
    public class GroupGetDto
    {
        public int Id { get; set; }
        public string No { get; set; }
        public List<StudentItemInGroupGetDto> Students { get; set; }
        public List<TeacherItemInGroupGetDto > Teachers { get; set; }
    }

    public class StudentItemInGroupGetDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int AvgPoint { get; set; }
    }

    public class TeacherItemInGroupGetDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Subject { get; set; }
    }
}
