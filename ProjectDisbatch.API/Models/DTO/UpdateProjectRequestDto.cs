using System.ComponentModel.DataAnnotations;

namespace ProjectDisbatch.API.Models.DTO
{
    public class UpdateProjectRequestDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 characters")]
        public string Name { get; set; }

        [MaxLength(200, ErrorMessage = "Description has to be a maximum of 200 characters")]
        public string? Description { get; set; }

        [Required]
        public Guid DepartmentId { get; set; }

        [Required]
        public Guid ProjectTypeId { get; set; }
    }
}
