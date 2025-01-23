namespace ProjectDisbatch.API.Models.DTO
{
    public class DepartmentDto
    {
        //Exposed the relevant properties from the Domain model (all of them in this case)

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }

    }
}
