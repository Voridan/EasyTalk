using System.Linq.Expressions;
using DAL.Models;

namespace BLL.Services.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllProjectsAsync();

        Task<IEnumerable<Project>> GetProjectAsync(
            Expression<Func<Project, bool>> filter,
            Func<IQueryable<Project>, IOrderedQueryable<Project>> orderBy,
            string includeProperties);

        Task<Project?> GetProjectByIdAsync(Guid id);

        Task AddProjectAsync(Project project);

        void UpdateProject(Project project);

        Task DeleteProjectAsync(Guid id);

        void DeleteProject(Project project);
    }
}
