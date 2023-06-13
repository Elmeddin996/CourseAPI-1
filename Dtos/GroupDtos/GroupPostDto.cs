using System.ComponentModel.DataAnnotations;

namespace CourseApi.Dtos.GroupDtos
{
    public class GroupPostDto
    {
        [Required]
        [MaxLength(20)]
        public string No { get; set; }
    }
}
