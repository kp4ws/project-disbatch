namespace ProjectDisbatch.API.Models.DTO
{
    public class AddDepartmentRequestDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
    }
}
