namespace ProjectDisbatch.API.Models.Domain
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        //Foreign keys
        public Guid DepartmentId { get; set; }
        public Guid ProjectTypeId { get; set; }

        //Navigation properties
        public Department Department { get; set; }
        public ProjectType ProjectType { get; set; }
    }
}
