using BLL.Models;
using BLL.Services.Implementations;
using BLL.Services.Interfaces;
using DAL.Data;
using DAL.Models;
using DAL.Repositories;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyTalk.Tests.Services
{

    public class ProjectServiceTests
    {
        private Mock<ProjectRepository>? _projectRepo;
        private ProjectService? _projectService;
        private EasyTalkContext? _dbContext;

        public ProjectServiceTests()
        {
            _dbContext = Mock.Of<EasyTalkContext>();
            _projectRepo = new Mock<ProjectRepository>(_dbContext);
            _projectService = new ProjectService(_projectRepo.Object);
        }

        [Fact]
        public async Task ProjectService_AddProjectAsync_CallsReposytoryAddAsync()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var project = new Project()
            {
                Id = projectId,
                Name = "Easy Talk"
            };

            _projectRepo.Setup(x => x.AddAsync(project));
            // Act
            await _projectService.AddProjectAsync(project);

            // Assert
            _projectRepo.Verify(x => x.AddAsync(project), Times.Once);
        }

        [Fact]
        public async Task ProjectService_DeleteProjectAsync_CallsReposytoryDeleteAsync()
        {
            // Arrange
            var projectId = Guid.NewGuid();

            _projectRepo.Setup(x => x.DeleteAsync(projectId));
            // Act
            await _projectService.DeleteProjectAsync(projectId);

            // Assert
            _projectRepo.Verify(x => x.DeleteAsync(projectId), Times.Once);
        }

        [Fact]
        public void ProjectService_DeleteProject_CallsReposytoryDelete()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var project = new Project()
            {
                Id = projectId,
                Name = "Easy Talk"
            };

            _projectRepo.Setup(x => x.Delete(project));
            // Act
            _projectService.DeleteProject(project);

            // Assert
            _projectRepo.Verify(x => x.Delete(project), Times.Once);
        }

        [Fact]
        public async Task ProjectService_GetAllProjectsAsync_CallsReposytoryGetAllAsync()
        {
            // Arrange
            var projectsFromRepository = new List<Project>
            {
                new Project { Name = "Name1"},
                new Project { Name = "Name2"},
                new Project { Name = "Name3"}
            };

            _projectRepo.Setup(x => x.GetAllAsync())
                .ReturnsAsync(projectsFromRepository);

            // Act
            var result = await _projectService.GetAllProjectsAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(projectsFromRepository.Count);
            _projectRepo.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task ProjectService_GetProjectByIdAsync_CallsReposytoryGetByIdAsync()
        {
            // Arrange
            var projectID = Guid.NewGuid();
            var existingProject = new Project()
            {
                Id = projectID,
                Name = "EasyTalk"
            };

            _projectRepo.Setup(x => x.GetByIdAsync(projectID))
                .ReturnsAsync(existingProject);
            // Act
            var result = await _projectService.GetProjectByIdAsync(projectID);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Project>();
            result.Id.Should().Be(projectID);
            result.Name.Should().Be("EasyTalk");
            _projectRepo.Verify(x => x.GetByIdAsync(projectID), Times.Once);

        }

        [Fact]
        public void ProjectService_UpdateProject_CallsRepositoryUpdate()
        {
            // Arrange
            var project = new Project
            {
                Id = Guid.NewGuid(),
                Name = "EasyTalk"
            };

            // Act
            _projectService.UpdateProject(project);

            // Assert
            _projectRepo.Verify(x => x.Update(project), Times.Once);
        }

        [Fact]
        public async Task GetProjectAsync_ReturnsFilteredProjects()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var projectName = "TestProject";
            var filteredProjects = new List<Project>
            {
                new Project { Id = projectId, Name = projectName, /* other properties */ },
                // Add more mocked projects as needed
            };

            // Mock the project repository
            _projectRepo.Setup(repo => repo.GetAsync(
                    It.IsAny<Expression<Func<Project, bool>>>(),
                    It.IsAny<Func<IQueryable<Project>, IOrderedQueryable<Project>>>(),
                    It.IsAny<string>())
                )
                .ReturnsAsync(filteredProjects);

            // Define a filter
            Expression<Func<Project, bool>> filter = p => p.Id == projectId;

            // Act
            var result = await _projectService.GetProjectAsync(filter, null, "");

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<Project>>(result);
            Assert.Single(result); // Ensure that only one project is returned based on the filter
            Assert.Equal(projectId, result.First().Id); // Ensure the correct project is returned
            // Add more assertions as needed
        }
    }
}

