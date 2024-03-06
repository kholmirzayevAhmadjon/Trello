using Project_menegment_tools.Enums;
using Project_menegment_tools.Extentions;
using Project_menegment_tools.Helps;
using Project_menegment_tools.Interfaces;
using Project_menegment_tools.Models.Tasks;

namespace Project_menegment_tools.Services;

public class TasksService : ITasksService
{
    private List<Tasks> taskList;
    public async ValueTask<TasksViewModel> CreateAsync(TasksCreationModel tasks)
    {
        taskList = await FileIO.ReadAsync<Tasks>(Constants.TASKS_PATH);

        var existTask = taskList.Create<Tasks>(tasks.ToMap());
        await FileIO.WriteAsync(Constants.TASKS_PATH, taskList);

        return existTask.ToMap();
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        taskList = await FileIO.ReadAsync<Tasks>(Constants.TASKS_PATH);
        var existTask = taskList.FirstOrDefault(task => task.Id == id && !task.IsDeleted)
            ??throw new Exception($"This Task is not found with ID = {id}");

        existTask.IsDeleted = true;
        await FileIO.WriteAsync(Constants.TASKS_PATH, taskList);

        return true;
    }

    public async ValueTask<IEnumerable<TasksViewModel>> GetAllAsync()
    {
        taskList = await FileIO.ReadAsync<Tasks>(Constants.TASKS_PATH);
        return taskList.Where(task => !task.IsDeleted).ToList().ToMap();
    }

    public async ValueTask<TasksViewModel> GetByIdAsync(long id)
    {
        taskList = await FileIO.ReadAsync<Tasks>(Constants.TASKS_PATH);
        var existTask = taskList.FirstOrDefault(task => task.Id == id && !task.IsDeleted)
            ?? throw new Exception($"This Task is not found with ID = {id}");

        return existTask.ToMap();
    }

    public async ValueTask<TasksViewModel> PerformTaskAsync(long teamMemberId, Status status)
    {
        taskList = await FileIO.ReadAsync<Tasks>(Constants.TASKS_PATH);
        var existTask = taskList.FirstOrDefault(task => task.TemMemberId == teamMemberId && !task.IsDeleted)
            ?? throw new Exception($"This Task is not found with TeamMemberId = {teamMemberId}");

        existTask.Status = status;

        FileIO.WriteAsync(Constants.TASKS_PATH, taskList);


        return existTask.ToMap() ;
    }

    public async ValueTask<TasksViewModel> UpdateAsync(long id, TasksUpdateModel tasks, bool isUsedDeleted = false)
    {
        taskList = await FileIO.ReadAsync<Tasks>(Constants.TASKS_PATH);
        var existTask = taskList.FirstOrDefault(task => task.Id == id && !task.IsDeleted)
            ?? throw new Exception($"This Task is not found with ID = {id}");

        existTask.Id = id;
        existTask.Title = tasks.Title;
        existTask.Description = tasks.Description;
        existTask.Status = tasks.Status;
        existTask.Priority = tasks.Priority;
        existTask.ProjectId = tasks.ProjectId;
        existTask.TemMemberId = tasks.TemMemberId;
        existTask.UpdatedAt = DateTime.UtcNow;

        await FileIO.WriteAsync(Constants.TASKS_PATH, taskList);

        return existTask.ToMap();
    }

    public async ValueTask<IEnumerable<TasksViewModel>> ViewTaskByTeamMemberIdAsync(long teamMemberId)
    {
        taskList = await FileIO.ReadAsync<Tasks>(Constants.TASKS_PATH);
        var list = new List<Tasks>();
        foreach(var tasks in taskList)
        {
            if(tasks.TemMemberId == teamMemberId)
            {
              list.Add(tasks);
            }
        }

        return list.ToMap();
    }
}
