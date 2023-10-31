using BLL.Services.Interfaces;
using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly ProjectRepository _projectRepo;

        public ProjectService(ProjectRepository projectRepo)
        {
            _projectRepo = projectRepo;
        }

        public async Task AddProjectAsync(Project project)
        {
            await _projectRepo.AddAsync(project);
        }

        public async Task DeleteProjectAsync(Guid id)
        {
            await _projectRepo.DeleteAsync(id);
        }

        public void DeleteProject(Project project)
        {
            _projectRepo.Delete(project);
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await _projectRepo.GetAllAsync();
        }

        public async Task<IEnumerable<Project>> GetProjectAsync(
            Expression<Func<Project, bool>> filter, 
            Func<IQueryable<Project>, IOrderedQueryable<Project>> orderBy, 
            string includeProperties)
        {
            return await _projectRepo.GetAsync(filter, orderBy, includeProperties);
        }

        public async Task<Project?> GetProjectByIdAsync(Guid id)
        {
            return await _projectRepo.GetByIdAsync(id);
        }

        public void UpdateProject(Project project)
        {
            _projectRepo.Update(project);
        }
    }
}
