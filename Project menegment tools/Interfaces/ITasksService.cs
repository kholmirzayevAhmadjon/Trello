using Project_menegment_tools.Enums;
using Project_menegment_tools.Models.Tasks;

namespace Project_menegment_tools.Interfaces;

public interface ITasksService
{
    /// <summary>
    /// Create New Tasks
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    ValueTask<TasksViewModel> CreateAsync(TasksCreationModel tasks);

    /// <summary>
    /// Update exist Tasks
    /// </summary>
    /// <param name="id"></param>
    /// <param name="user"></param>
    /// <param name="isUsedDeleted"></param>
    /// <returns></returns>
    ValueTask<TasksViewModel> UpdateAsync(long id, TasksUpdateModel tasks, bool isUsedDeleted);

    /// <summary>
    /// Delete exist Tasks via 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    ValueTask<bool> DeleteAsync(long id);

    /// <summary>
    /// Search for Tasks by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    ValueTask<TasksViewModel> GetByIdAsync(long id);

    /// <summary>
    /// View all Tasks
    /// </summary>
    /// <returns></returns>
    ValueTask<IEnumerable<TasksViewModel>> GetAllAsync();

    /// <summary>
    /// Team Member See what task is assigned to him by his id number
    /// </summary>
    /// <returns></returns>
    ValueTask<IEnumerable<TasksViewModel>> ViewTaskByTeamMemberIdAsync(long teamMemberId);

    /// <summary>
    /// TeamMember to inform about the start and completion of the task assigned to him
    /// </summary>
    /// <param name="teamMemberId"></param>
    /// <returns></returns>
    ValueTask<TasksViewModel> PerformTaskAsync(long teamMemberId, Status status);
}
