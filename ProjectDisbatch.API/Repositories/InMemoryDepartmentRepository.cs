using ProjectDisbatch.API.Models.Domain;

namespace ProjectDisbatch.API.Repositories
{
    public class InMemoryDepartmentRepository : IDepartmentRepository
    {
        public Task<Department> CreateAsync(Department departmentDomainModel)
        {
            throw new NotImplementedException();
        }

        public Task<Department?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Department>> GetAllAsync()
        {
            return new List<Department> {
                new Department()
                {
                    Id = Guid.NewGuid(),
                    Name = "Test",
                    Code = "Test",
                    Description = "Test"
                }
            };
        }

        public Task<Department?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Department?> UpdateAsync(Guid id, Department departmentDomainModel)
        {
            throw new NotImplementedException();
        }
    }
}
