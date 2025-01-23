namespace ProjectDisbatch.API.Models.DTO
{
    public class UpdateDepartmentRequestDto
    {
        //Only include properties that are allowed to be updated.
        public string Name { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
    }
}
