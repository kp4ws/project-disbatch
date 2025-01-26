using System.ComponentModel.DataAnnotations;

namespace ProjectDisbatch.API.Models.DTO
{
    public class AddDepartmentRequestDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 characters")]
        public string Name { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Code has to be a minimum of 3 characters")]
        [MaxLength(6, ErrorMessage = "Code has to be a maximum of 6 characters")]
        public string Code { get; set; }

        [MaxLength(200, ErrorMessage = "Description has to be a maximum of 200 characters")]
        public string? Description { get; set; }
    }
}
