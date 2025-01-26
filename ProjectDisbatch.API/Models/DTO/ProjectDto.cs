namespace ProjectDisbatch.API.Models.DTO
{
    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public DepartmentDto Department { get; set; }
        public ProjectTypeDto ProjectType { get; set; }
    }
}
