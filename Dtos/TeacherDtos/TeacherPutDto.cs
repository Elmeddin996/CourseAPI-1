using System.ComponentModel.DataAnnotations;

namespace CourseApi.Dtos.TeacherDtos
{
    public class TeacherPutDto
    {
        [Required]
        [MaxLength(25)]
        public string FullName { get; set; }
        [Required]
        [MaxLength(100)]
        public string Subject { get; set; }
        [Required]
        public int GroupId { get; set; }
    }
}
