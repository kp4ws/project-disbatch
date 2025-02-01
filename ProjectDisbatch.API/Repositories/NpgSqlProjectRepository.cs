using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectDisbatch.API.Data;
using ProjectDisbatch.API.Models.Domain;

namespace ProjectDisbatch.API.Repositories
{
    public class NpgSqlProjectRepository : IProjectRepository
    {
        private readonly ProjectDisbatchDbContext dbContext;

        public NpgSqlProjectRepository(ProjectDisbatchDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Project> CreateAsync(Project projectDomainModel)
        {
            await dbContext.AddAsync(projectDomainModel);
            await dbContext.SaveChangesAsync();
            return projectDomainModel;
        }

        public async Task<List<Project>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 1000)
        {
            var projects = dbContext.Projects.Include("Department").Include("ProjectType").AsQueryable();

            //Filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                //TODO Do we need to sanitize filterOn and filterQuery parameters?
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    //NOTE: filterQuery is case sensitive
                    projects = projects.Where(x => x.Name.Contains(filterQuery));
                }
            }

            //Sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                //TODO Do we need to sanitize input parameters?
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    projects = isAscending ? projects.OrderBy(x => x.Name) : projects.OrderByDescending(x => x.Name);
                }
            }

            //Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await projects.Skip(skipResults).Take(pageSize).ToListAsync();
            //You can make Include typesafe by doing (x => x.Department)
            //return await dbContext.Projects.Include("Department").Include("ProjectType").ToListAsync();
        }

        public async Task<Project?> GetByIdAsync(Guid id)
        {
            return await dbContext.Projects
                .Include("Department")
                .Include("ProjectType")
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Project?> UpdateAsync(Guid id, Project projectDomainModel)
        {
            var existingProject = await dbContext.Projects.FirstOrDefaultAsync(x => x.Id == id);
            if (existingProject == null)
            {
                return null;
            }

            existingProject.Name = projectDomainModel.Name;
            existingProject.Description = projectDomainModel.Description;
            existingProject.DepartmentId = projectDomainModel.DepartmentId;
            existingProject.ProjectTypeId = projectDomainModel.ProjectTypeId;

            await dbContext.SaveChangesAsync();
            return existingProject;
        }

        public async Task<Project?> DeleteAsync(Guid id)
        {
            //Get Domain Model
            var projectDomainModel = await dbContext.Projects.FirstOrDefaultAsync(x => x.Id == id);

            //Check if exists
            if (projectDomainModel == null)
            {
                return null;
            }

            dbContext.Remove(projectDomainModel);
            await dbContext.SaveChangesAsync();
            return projectDomainModel;
        }
    }
}
