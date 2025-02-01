using ProjectDisbatch.API.Models.Domain;

namespace ProjectDisbatch.API.Repositories
{
    public interface IProjectRepository
    {
        Task<Project> CreateAsync(Project projectDomainModel);
        Task<List<Project>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? 
            sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 1000);
        Task<Project?> GetByIdAsync(Guid id);
        Task<Project?> UpdateAsync(Guid id, Project projectDomainModel);
        Task<Project?> DeleteAsync(Guid id);
    }
}
