using ProjectDisbatch.API.Models.Domain;

namespace ProjectDisbatch.API.Repositories
{
    public interface IProjectRepository
    {
        Task<Project> CreateAsync(Project projectDomainModel);
        Task<List<Project>> GetAllAsync();
        Task<Project?> GetByIdAsync(Guid id);
        Task<Project?> UpdateAsync(Guid id, Project projectDomainModel);
        Task<Project?> DeleteAsync(Guid id);
    }
}
