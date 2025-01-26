using Microsoft.EntityFrameworkCore;
using ProjectDisbatch.API.Data;
using ProjectDisbatch.API.Models.Domain;
using ProjectDisbatch.API.Models.DTO;

namespace ProjectDisbatch.API.Repositories
{
    public class NpgSqlDepartmentRepository : IDepartmentRepository
    {
        private readonly ProjectDisbatchDbContext dbContext;

        public NpgSqlDepartmentRepository(ProjectDisbatchDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Department>> GetAllAsync()
        {
            return await dbContext.Departments.ToListAsync();
        }

        public async Task<Department?> GetByIdAsync(Guid id)
        {
            return await dbContext.Departments.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Department> CreateAsync(Department departmentDomainModel)
        {
            await dbContext.Departments.AddAsync(departmentDomainModel);
            await dbContext.SaveChangesAsync();
            return departmentDomainModel;
        }

        public async Task<Department?> UpdateAsync(Guid id, Department departmentDomainModel)
        {
            var existingDepartment = await dbContext.Departments.FirstOrDefaultAsync(x => x.Id == id);
            if (existingDepartment == null)
            {
                return null;
            }
            existingDepartment.Code = departmentDomainModel.Code;
            existingDepartment.Name = departmentDomainModel.Name;
            existingDepartment.Description = departmentDomainModel.Description;

            await dbContext.SaveChangesAsync();
            return existingDepartment;
        }

        public async Task<Department?> DeleteAsync(Guid id)
        {
            var existingDepartment = await dbContext.Departments.FirstOrDefaultAsync(x => x.Id == id);
            if (existingDepartment == null)
            {
                return null;
            }

            dbContext.Remove(existingDepartment); //Remove cannot be asynchronous
            await dbContext.SaveChangesAsync();
            return existingDepartment;
        }
    }
}
