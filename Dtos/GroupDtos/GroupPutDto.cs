using System.ComponentModel.DataAnnotations;

namespace CourseApi.Dtos.GroupDtos
{
    public class GroupPutDto
    {
        [Required]
        [MaxLength(20)]
        public string No { get; set; }
    }
}
