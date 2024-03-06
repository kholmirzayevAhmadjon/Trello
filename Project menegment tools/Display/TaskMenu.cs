using Microsoft.VisualBasic;
using Project_menegment_tools.Enums;
using Project_menegment_tools.Models.Projects;
using Project_menegment_tools.Models.Tasks;
using Project_menegment_tools.Services;
using Spectre.Console;
using System.Xml.Linq;
using Status = Project_menegment_tools.Enums.Status;

namespace Project_menegment_tools.Display;

public class TaskMenu
{
    private readonly TasksService _tasksService;
    private readonly UserService _userService;
    private readonly ProjectServise _projectServise;

    public TaskMenu(TasksService tasksService, UserService userService, ProjectServise projectServise)
    {
        _tasksService = tasksService;
        _userService = userService;
        _projectServise = projectServise;
    }

    public async void Create()
    {
        Console.Clear();
        Console.Write("Enter Title :");
        string title = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter Title :");
            title = Console.ReadLine();
        }

        Console.Write("Descreption :");
        string descreption = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(descreption))
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter Descreption :");
            descreption = Console.ReadLine();
        }

        Console.Write("Enter Projecte Id :");
        long projectId;
        while (!long.TryParse(Console.ReadLine(), out projectId) || projectId < 0)
        {
            Console.WriteLine("Was entered in the wrong format .Try agin !");
            Console.Write("Enter Project Id :");
        }

        Console.Write("Enter TeamMember Id :");
        long teamMemberId;
        while (!long.TryParse(Console.ReadLine(), out teamMemberId) || teamMemberId < 0)
        {
            Console.WriteLine("Was entered in the wrong format .Try agin !");
            Console.Write("Enter TeamMember Id :");
        }

        Console.Write("Enter the tsk DueDate example year/month/day: ");
        DateTime dueDate;
        while (!DateTime.TryParse(Console.ReadLine(), out dueDate) && dueDate < DateTime.UtcNow)
        {
            Console.WriteLine("Was entered in the wrong format .Try agin !");
            Console.Write("Please Enter the project start time example year/month/day: ");
        }

        Console.WriteLine("1.High, 2.Medium, 3.Low, ");
        int choice;
        Console.Write("Enter Priority:");
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 4)
        {
            Console.WriteLine("Was entered in the wrong format .Try agin !");
            Console.Write("Enter Priority :");
        }
        Priority[] value = (Priority[])Enum.GetValues(typeof(Priority));

        TasksCreationModel task = new TasksCreationModel()
        {
            Title = title,
            Description = descreption,
            ProjectId = projectId,
            TemMemberId = teamMemberId,
            DueDate = dueDate,
            Status = Status.NotStarted,
            Priority = value[choice]
        };
        try
        {
            _projectServise.GetByIdAsync(projectId);
            var user = await _userService.GetByIdAsync(teamMemberId);
            if (user.Role != Role.TeamMember)
            {
                await Console.Out.WriteLineAsync($"TeamMember at this id = {teamMemberId} does not exist");
            }

            else
            {
                var createTask = await _tasksService.CreateAsync(task);
                AnsiConsole.Markup("[orange3] Registration completed successfully! [/]\n");
                var table = new Table();

                table.AddColumn("[slateblue1]Id[/]");
                table.AddColumn("[slateblue1]Title[/]");
                table.AddColumn("[slateblue1]Descreption[/]");
                table.AddColumn("[slateblue1]ProjectId[/]");
                table.AddColumn("[slateblue1]Status[/]");
                table.AddColumn("[slateblue1]Priority[/]");
                table.AddColumn("[slateblue1]TeamMemberId[/]");
                table.AddColumn("[slateblue1]DueDate[/]");

                table.AddRow(createTask.Id.ToString(), createTask.Title, createTask.Description, createTask.ProjectId.ToString(),
                    createTask.Status.ToString(), createTask.Priority.ToString(), createTask.TemMemberId.ToString(), createTask.DueDate.ToString());
                AnsiConsole.Write(table);
                Console.WriteLine("Enter any keyword to continue");
                Console.ReadKey();
                Console.Clear();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Thread.Sleep(800);
        }
    }

    public async void Delete()
    {
        Console.Clear();
        Console.Write("Enter task Id to delete :");
        int id;
        while (!int.TryParse(Console.ReadLine(), out id) || id < 0)
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter task Id to delete :");
        }
        try
        {
            await _tasksService.DeleteAsync(id);
            AnsiConsole.Markup("[orange3] Succesful deleted [/]\n"); ;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async void GetById()
    {
        Console.Clear();
        Console.Write("Enter task Id :");
        int id;
        while (!int.TryParse(Console.ReadLine(), out id) || id < 0)
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter task Id :");
        }
        try
        {
            var result = await _tasksService.GetByIdAsync(id);
            var table = new Table();

            table.AddColumn("[slateblue1]Id[/]");
            table.AddColumn("[slateblue1]Title[/]");
            table.AddColumn("[slateblue1]Descreption[/]");
            table.AddColumn("[slateblue1]ProjectId[/]");
            table.AddColumn("[slateblue1]Status[/]");
            table.AddColumn("[slateblue1]Priority[/]");
            table.AddColumn("[slateblue1]TeamMemberId[/]");
            table.AddColumn("[slateblue1]DueDate[/]");

            table.AddRow(result.Id.ToString(), result.Title, result.Description, result.ProjectId.ToString(),
                    result.Status.ToString(), result.Priority.ToString(), result.TemMemberId.ToString(), result.DueDate.ToString());
            AnsiConsole.Write(table);
            Console.WriteLine("Enter any keyword to continue");
            Console.ReadKey();
            Console.Clear();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async void PerformTask()
    {
        Console.Clear();
        Console.Write("Enter Team Member Id: ");
        int id;
        while (!int.TryParse(Console.ReadLine(), out id) || id < 0)
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter Team Member Id: ");
        }

        Console.WriteLine("1.NotStarted, 2.InProgress, 3.Completed");
        int choise;
        Console.Write("Enter Status:");
        while (!int.TryParse(Console.ReadLine(), out choise) || choise < 0 || choise > 4)
        {
            Console.WriteLine("Was entered in the wrong format .Try agin !");
            Console.Write("Enter Status :");
        }
        var status =new Status();
        if(choise == 1) status = Status.NotStarted ;
        if(choise == 2) status = Status.InProgress ;
        if(choise == 3) status = Status.Completed ;


        try
        {
            var result = await _tasksService.PerformTaskAsync(id, status);
            var table = new Table();

            table.AddColumn("[slateblue1]Id[/]");
            table.AddColumn("[slateblue1]Title[/]");
            table.AddColumn("[slateblue1]Descreption[/]");
            table.AddColumn("[slateblue1]ProjectId[/]");
            table.AddColumn("[slateblue1]Status[/]");
            table.AddColumn("[slateblue1]Priority[/]");
            table.AddColumn("[slateblue1]TeamMemberId[/]");
            table.AddColumn("[slateblue1]DueDate[/]");

            table.AddRow(result.Id.ToString(), result.Title, result.Description, result.ProjectId.ToString(),
                    result.Status.ToString(), result.Priority.ToString(), result.TemMemberId.ToString(), result.DueDate.ToString());
            AnsiConsole.Write(table);
            Console.WriteLine("Enter any keyword to continue");
            Console.ReadKey();
            Console.Clear();
        }

        catch (Exception ex) 
        {
            Console.WriteLine(ex.Message);
        }

    }

    public async void GetAll()
    {
        Console.Clear();
        var table = new Table();

        table.AddColumn("[slateblue1]Id[/]");
        table.AddColumn("[slateblue1]Title[/]");
        table.AddColumn("[slateblue1]Descreption[/]");
        table.AddColumn("[slateblue1]ProjectId[/]");
        table.AddColumn("[slateblue1]Status[/]");
        table.AddColumn("[slateblue1]Priority[/]");
        table.AddColumn("[slateblue1]TeamMemberId[/]");
        table.AddColumn("[slateblue1]DueDate[/]");

        var project = await _tasksService.GetAllAsync();

        foreach (var result in project)
        {
            table.AddRow(result.Id.ToString(), result.Title, result.Description, result.ProjectId.ToString(),
                    result.Status.ToString(), result.Priority.ToString(), result.TemMemberId.ToString(), result.DueDate.ToString());
        }

        AnsiConsole.Write(table);
        Console.WriteLine("Enter any keyword to continue");
        Console.ReadKey();
        Console.Clear();
    }

    public async void Update()
    {
        Console.Clear();
        Console.Write("Enter task Id to update :");
        long id;
        while (!long.TryParse(Console.ReadLine(), out id) || id < 0)
        {
            Console.WriteLine("Was entered in the wrong format .Try agin !");
            Console.Write("Enter task Id to update :");
        }
        Console.Write("Enter Title :");
        string title = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter Title :");
            title = Console.ReadLine();
        }

        Console.Write("Descreption :");
        string descreption = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(descreption))
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter Descreption :");
            descreption = Console.ReadLine();
        }

        Console.Write("Enter Projecte Id :");
        long projectId;
        while (!long.TryParse(Console.ReadLine(), out projectId) || projectId < 0)
        {
            Console.WriteLine("Was entered in the wrong format .Try agin !");
            Console.Write("Enter Project Id :");
        }

        Console.Write("Enter TeamMember Id :");
        long teamMemberId;
        while (!long.TryParse(Console.ReadLine(), out teamMemberId) || teamMemberId < 0)
        {
            Console.WriteLine("Was entered in the wrong format .Try agin !");
            Console.Write("Enter TeamMember Id :");
        }

        Console.Write("Enter the tsk DueDate example year/month/day: ");
        DateTime dueDate;
        while (!DateTime.TryParse(Console.ReadLine(), out dueDate) && dueDate < DateTime.UtcNow)
        {
            Console.WriteLine("Was entered in the wrong format .Try agin !");
            Console.Write("Please Enter the project start time example year/month/day: ");
        }

        Console.WriteLine("1.NotStarted, 2.InProgress, 3.Completed");
        int choise;
        Console.Write("Enter Status:");
        while (!int.TryParse(Console.ReadLine(), out choise) || choise < 0 || choise > 4)
        {
            Console.WriteLine("Was entered in the wrong format .Try agin !");
            Console.Write("Enter Status :");
        }
        Status[] values = (Status[])Enum.GetValues(typeof(Status));

        Console.WriteLine("1.High, 2.Medium, 3.Low, ");
        int choice;
        Console.Write("Enter Priority:");
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 4)
        {
            Console.WriteLine("Was entered in the wrong format .Try agin !");
            Console.Write("Enter Priority :");
        }
        Priority[] value = (Priority[])Enum.GetValues(typeof(Priority));

        TasksUpdateModel task = new TasksUpdateModel()
        {
            Title = title,
            Description = descreption,
            ProjectId = projectId,
            TemMemberId = teamMemberId,
            Status = values[choise],
            Priority = value[choice]

        };

        try
        {
            var updateTask = await _tasksService.UpdateAsync(id, task);
            AnsiConsole.Markup("[orange3]Succesful updated[/]\n");
            var table = new Table();

            table.AddColumn("[slateblue1]Id[/]");
            table.AddColumn("[slateblue1]Title[/]");
            table.AddColumn("[slateblue1]Descreption[/]");
            table.AddColumn("[slateblue1]ProjectId[/]");
            table.AddColumn("[slateblue1]Status[/]");
            table.AddColumn("[slateblue1]Priority[/]");
            table.AddColumn("[slateblue1]TeamMemberId[/]");
            table.AddColumn("[slateblue1]DueDate[/]");

            table.AddRow(updateTask.Id.ToString(), updateTask.Title, updateTask.Description, updateTask.ProjectId.ToString(),
                    updateTask.Status.ToString(), updateTask.Priority.ToString(), updateTask.TemMemberId.ToString(), updateTask.DueDate.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            // Thread.Sleep(1000);
        }
        Console.WriteLine("Enter any keyword to continue");
        Console.ReadKey();
        Console.Clear();
    }

    public async void ViewTaskViewTaskByTeamMemberId()
    {
        Console.Clear();
        Console.Write("Enter Team Member id: ");
        long id;
        while (!long.TryParse(Console.ReadLine(), out id) || id < 0)
        {
            Console.WriteLine("Was entered in the wrong format .Try agin !");
            Console.Write("Enter Team Member id: ");
        }
        var table = new Table();
    
        table.AddColumn("[slateblue1]Id[/]");
        table.AddColumn("[slateblue1]Title[/]");
        table.AddColumn("[slateblue1]Descreption[/]");
        table.AddColumn("[slateblue1]ProjectId[/]");
        table.AddColumn("[slateblue1]Status[/]");
        table.AddColumn("[slateblue1]Priority[/]");
        table.AddColumn("[slateblue1]TeamMemberId[/]");
        table.AddColumn("[slateblue1]DueDate[/]");
       
       var project = await _tasksService.ViewTaskByTeamMemberIdAsync(id);
        foreach (var result in project)
        {
            table.AddRow(result.Id.ToString(), result.Title, result.Description, result.ProjectId.ToString(),
                    result.Status.ToString(), result.Priority.ToString(), result.TemMemberId.ToString(), result.DueDate.ToString());
        }

        AnsiConsole.Write(table);
        Console.WriteLine("Enter any keyword to continue");
        Console.ReadKey();
        Console.Clear();
    }

    public void Display()
    {
        bool circle = true;
        while (circle)
        {
            var category = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("[darkorange3_1]-- Welcome to trello --[/]")
            .PageSize(10)
            .AddChoices(new[] {
        "Create", "Delete", "Update", "GetById" , "GetAll", "Exit",
            }));
            switch (category)
            {
                case "Create":
                    Create();
                    break;
                case "Delete":
                    Delete();
                    break;
                case "Update":
                    Update();
                    break;
                case "GetById":
                    GetById();
                    break;
                case "GetAll":
                    GetAll();
                    break;
                case "Exit": circle = false; break;
            }
        }
    }
}
