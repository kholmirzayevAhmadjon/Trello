using Project_menegment_tools.Enums;
using Project_menegment_tools.Models.Projects;
using Project_menegment_tools.Models.Tasks;
using Project_menegment_tools.Models.User;
using Project_menegment_tools.Models.Users;

namespace Project_menegment_tools.Extentions;

public static class MapperExtention
{
    #region User mappers
    public static User ToMap(this UserCreationModel model)
   {
        return new User
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Password = model.Password,
            Role = model.Role,
        };
   }

    public static UserUpdateModel ToMapped(this UserCreationModel model)
    {
        return new UserUpdateModel
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Password = model.Password,
            Role = model.Role,
        };
    }

    public static User ToMap(this UserUpdateModel model, long id)
    {
        return new User
        {
            Id = id,
            FirstName = model.FirstName,
            LastName= model.LastName,
            Email= model.Email,
            Password= model.Password,
            Role= model.Role,
        };
    }

    public static UserViewModel ToMap(this User model)
    {
        return new UserViewModel
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Role = model.Role
        };
    }

    public static List<UserViewModel> ToMap(this List<User> models)
    {
        var result = new List<UserViewModel>();

        foreach(var model in models)
        {
            result.Add(new UserViewModel
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Role = model.Role

            });
        }
        return result;       
    }
    #endregion

    #region Project mappers
    public static Project ToMap(this ProjectCreationModel model)
    {
        return new Project
        {
            Name = model.Name,
            Description = model.Description,
            StartTime = model.StartTime,
            EndTime = model.EndTime,
            Status = model.Status,
            ProjectMenegerId = model.ProjectMenegerId,
        };
    }

    public static Project ToMap(this ProjectUpdateModel model, long id)
    {
        return new Project
        {
            Name = model.Name,
            Description = model.Description,
            EndTime = model.EndTime,
            Status = model.Status,
           ProjectMenegerId= model.ProjectMenegerId,
        };
    }

    public static ProjectUpdateModel ToMapped(this ProjectCreationModel model)
    {
        return new ProjectUpdateModel
        {
            Name = model.Name,
            Description = model.Description,
            EndTime = model.EndTime,
            Status = model.Status,
            ProjectMenegerId = model.ProjectMenegerId,
        };
    }

    public static ProjectViewModel ToMap(this Project model)
    {
        return new ProjectViewModel
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            StartTime = model.StartTime,
            EndTime = model.EndTime,
            Status = model.Status,
            ProjectMenegerId = model.ProjectMenegerId,
        };
    }

    public static List<ProjectViewModel> ToMap(this List<Project> models)
    {
        var result = new List<ProjectViewModel>();

        foreach (var model in models)
        {
            result.Add(new ProjectViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                StartTime= model.StartTime,
                EndTime= model.EndTime,
                Status= model.Status,
                ProjectMenegerId= model.ProjectMenegerId,
            });
        }
        return result;
    }

    #endregion

    #region Tasks meppers
    public static Tasks ToMap(this TasksCreationModel model)
    {
        return new Tasks
        {
            Title = model.Title,
            Description = model.Description,
            DueDate = model.DueDate,
            ProjectId = model.ProjectId,
            Priority = model.Priority,
            Status = model.Status,
            TemMemberId = model.TemMemberId
        };
    }

    public static Tasks ToMap(this TasksUpdateModel model, long id)
    {
        return new Tasks
        {
            Title = model.Title,
            Description = model.Description,
            ProjectId = model.ProjectId,
            Priority = model.Priority,
            Status = model.Status,
            TemMemberId = model.TemMemberId

        };
    }

    public static TasksViewModel ToMap(this Tasks model)
    {
        return new TasksViewModel
        {
            Id =model.Id, 
            Title = model.Title,
            Description = model.Description,
            DueDate = model.DueDate,
            ProjectId = model.ProjectId,
            Priority = model.Priority,
            Status = model.Status,
            TemMemberId = model.TemMemberId
        };
    }

    public static TasksUpdateModel ToMapper(this TasksCreationModel model)
    {
        return new TasksUpdateModel
        {
            Title = model.Title,
            Description = model.Description,
            ProjectId = model.ProjectId,
            Priority = model.Priority,
            Status = model.Status,
            TemMemberId = model.TemMemberId
        };
    }

    public static List<TasksViewModel> ToMap(this List<Tasks> models)
    {
        var result = new List<TasksViewModel>();

        foreach (var model in models)
        {
            result.Add( new TasksViewModel
            {
                Id=model.Id,
                Title=model.Title,
                Description=model.Description,
                DueDate=model.DueDate,
                Priority = model.Priority,
                Status=model.Status,
                ProjectId=model.ProjectId,
                TemMemberId=model.TemMemberId
            });
        }

        return result;
    }
    #endregion
}
