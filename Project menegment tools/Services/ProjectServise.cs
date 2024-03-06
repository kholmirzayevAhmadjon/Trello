using Project_menegment_tools.Enums;
using Project_menegment_tools.Extentions;
using Project_menegment_tools.Helps;
using Project_menegment_tools.Interfaces;
using Project_menegment_tools.Models.Projects;

namespace Project_menegment_tools.Services;

public class ProjectServise : IProjectService
{
    private List<Project> projects;
    public async ValueTask<ProjectViewModel> CreateAsync(ProjectCreationModel project)
    {
        projects = await FileIO.ReadAsync<Project>(Constants.PROJECT_PATH);
  
        var createProject = projects.Create<Project>(project.ToMap());
        await FileIO.WriteAsync(Constants.PROJECT_PATH, projects);
        return createProject.ToMap();
    }
  
    public async ValueTask<bool> DeleteAsync(long id)
    {
        projects = await FileIO.ReadAsync<Project>(Constants.PROJECT_PATH);
        var existDelete = projects.FirstOrDefault(delete => delete.Id == id && !delete.IsDeleted)
            ?? throw new Exception($"This Project is not found with ID = {id}");
  
        existDelete.IsDeleted = true;
        existDelete.DeletedAt = DateTime.UtcNow;
        await FileIO.WriteAsync(Constants.PROJECT_PATH, projects);
        return true;
    }
  
    public async ValueTask<IEnumerable<ProjectViewModel>> GetAllAsync()
    {
        projects = await FileIO.ReadAsync<Project>(Constants.PROJECT_PATH);
        return projects.Where(p => !p.IsDeleted).ToList().ToMap();
    }
  
    public async ValueTask<ProjectViewModel> GetByIdAsync(long id)
    {
        projects = await FileIO.ReadAsync<Project>(Constants.PROJECT_PATH);
        var existProject = projects.FirstOrDefault(p => p.Id == id && !p.IsDeleted)
            ?? throw new Exception($"This Project is not found with ID = {id}");
  
        return existProject.ToMap();
    }

    public async ValueTask<ProjectViewModel> PerformProject(long projectMenegerId, Status status)
    {
        projects = await FileIO.ReadAsync<Project>(Constants.PROJECT_PATH);
        var existProject = projects.FirstOrDefault(p => p.ProjectMenegerId == projectMenegerId && !p.IsDeleted)
            ?? throw new Exception($"This Project is not found with ProjectMenegerId = {projectMenegerId}");

        existProject.Status = status;

        await FileIO.WriteAsync(Constants.PROJECT_PATH, projects);

        return existProject.ToMap() ;
    }

    public async ValueTask<ProjectViewModel> UpdateAsync(long id, ProjectUpdateModel project, bool isUsedDeleted = false)
    {
        projects = await FileIO.ReadAsync<Project>(Constants.PROJECT_PATH);
        var existProject = projects.FirstOrDefault(p => p.Id == id && !p.IsDeleted)
             ?? throw new Exception($"This Project is not found with ID = {id}");
  
        existProject.Id = id;
        existProject.ProjectMenegerId = project.ProjectMenegerId;
        existProject.Name = project.Name;
        existProject.Status = project.Status;
        existProject.EndTime = project.EndTime;
        existProject.Description = project.Description;
        existProject.UpdatedAt = DateTime.UtcNow;
  
        await FileIO.WriteAsync(Constants.PROJECT_PATH, projects);
        return existProject.ToMap();
    }

    public async ValueTask<IEnumerable<ProjectViewModel>> ViewProjectByProjectMenegerIdAsync(long projectMenegerId)
    {
        projects = await FileIO.ReadAsync<Project>(Constants.PROJECT_PATH);

        var list = new List<Project>();
        foreach (var item in projects)
        {
            if(item.ProjectMenegerId == projectMenegerId)
            {
                list.Add(item);
            }
        }

        return list.ToMap();
    }
} 
  