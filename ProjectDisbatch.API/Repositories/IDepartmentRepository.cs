using ProjectDisbatch.API.Models.Domain;

namespace ProjectDisbatch.API.Repositories
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAllAsync();
        Task<Department?> GetByIdAsync(Guid id);
        Task<Department> CreateAsync(Department departmentDomainModel);
        Task<Department?> UpdateAsync(Guid id, Department departmentDomainModel);
        Task<Department?> DeleteAsync(Guid id);
    }
}
