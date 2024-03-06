using Project_menegment_tools.Enums;
using Project_menegment_tools.Models.Projects;

namespace Project_menegment_tools.Interfaces;

public interface IProjectService
{
    /// <summary>
    /// Create new project
    /// </summary>
    /// <param name="project"></param>
    /// <returns></returns>
    ValueTask<ProjectViewModel>  CreateAsync(ProjectCreationModel project);

    /// <summary>
    /// Update exit project
    /// </summary>
    /// <param name="id"></param>
    /// <param name="project"></param>
    /// <returns></returns>
    ValueTask<ProjectViewModel> UpdateAsync(long id, ProjectUpdateModel project, bool isUsedDeleted);

    /// <summary>
    /// Delete exist user via
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    ValueTask<bool> DeleteAsync(long id);

    /// <summary>
    /// Search for user by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    ValueTask<ProjectViewModel> GetByIdAsync(long id);

    /// <summary>
    /// View all registered users
    /// </summary>
    /// <returns></returns>
    ValueTask<IEnumerable<ProjectViewModel>> GetAllAsync();

    /// <summary>
    /// ProjectMemeger to see what Project is attached to it by its id number
    /// </summary>
    /// <param name="projectMenegerId"></param>
    /// <returns></returns>
    ValueTask<IEnumerable<ProjectViewModel>> ViewProjectByProjectMenegerIdAsync(long projectMenegerId);

    /// <summary>
    /// ProjectManager to inform about the start and completion of execution of the assigned Project
    /// </summary>
    /// <param name="projectMenegerId"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    ValueTask<ProjectViewModel> PerformProject(long projectMenegerId, Status status);
}
