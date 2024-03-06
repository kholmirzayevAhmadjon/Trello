using Project_menegment_tools.Enums;
using Project_menegment_tools.Interfaces;
using Project_menegment_tools.Models.Projects;
using Project_menegment_tools.Models.Users;
using Project_menegment_tools.Services;
using Spectre.Console;
using Status = Project_menegment_tools.Enums.Status;

namespace Project_menegment_tools.Display;

public class ProjectMenu
{
    private readonly ProjectServise _projectServise;
    private readonly UserService _userService;
    private readonly UserMenu _userMenu;
    private readonly TaskMenu _taskMenu;
    public ProjectMenu(ProjectServise projectServise, UserService userService, UserMenu userMenu, TaskMenu taskMenu)
    {
        _projectServise = projectServise;
        _userService = userService;
        _userMenu = userMenu;
        _taskMenu = taskMenu;
    }

    public async void Create()
    {
        Console.Clear();
        Console.Write("Enter Name :");
        string name = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter Name :");
            name = Console.ReadLine();
        }

        Console.Write("Descreption :");
        string descreption = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(descreption))
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter Descreption :");
            descreption = Console.ReadLine();
        }

        Console.Write("Enter ProjectMeneger Id :");
        long projectMenegerId;
        while (!long.TryParse(Console.ReadLine(), out projectMenegerId) || projectMenegerId < 0)
        {
            Console.WriteLine("Was entered in the wrong format .Try agin !");
            Console.Write("Enter ProjectMeneger Id :");
        }

        Console.Write("Enter the project start time example year/month/day: ");
        DateTime startTime;
        while (!DateTime.TryParse(Console.ReadLine(), out startTime) && startTime < DateTime.UtcNow)
        {
            Console.WriteLine("Was entered in the wrong format .Try agin !");
            Console.Write("Please Enter the project start time example year/month/day: ");
        }

        Console.Write("Enter the project end time example year/month/day: ");
        DateTime endTime;
        while (!DateTime.TryParse(Console.ReadLine(), out endTime) && endTime < startTime)
        {
            Console.WriteLine("Was entered in the wrong format .Try agin !");
            Console.Write("Please Enter the project end time example year/month/day: ");
        }

        ProjectCreationModel project = new ProjectCreationModel()
        {
            Name = name,
            Description = descreption,
            StartTime = startTime,
            EndTime = endTime,
            ProjectMenegerId = projectMenegerId,
            Status = Status.NotStarted,
        };
        try
        {
            _userService.GetByIdAsync(projectMenegerId);
            var createdProject = await _projectServise.CreateAsync(project);
            AnsiConsole.Markup("[orange3] Registration completed successfully! [/]\n");
            var table = new Table();

            table.AddColumn("[slateblue1]Id[/]");
            table.AddColumn("[slateblue1]Name[/]");
            table.AddColumn("[slateblue1]Descreption[/]");
            table.AddColumn("[slateblue1]StartTime[/]");
            table.AddColumn("[slateblue1]EndTme[/]");
            table.AddColumn("[slateblue1]Status[/]");
            table.AddColumn("[slateblue1]ProjectMenegerId[/]");

            table.AddRow(createdProject.Id.ToString(), createdProject.Name, createdProject.Description, createdProject.StartTime.ToString(),
                createdProject.EndTime.ToString(), createdProject.Status.ToString(), createdProject.ProjectMenegerId.ToString());
            AnsiConsole.Write(table);
            Console.WriteLine("Enter any keyword to continue");
            Console.ReadKey();
            Console.Clear();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Thread.Sleep(800);
        }


    }

    public async void PerformProject()
    {
        Console.Clear();
        Console.Write("Enter Project Meneger Id: ");
        int id;
        while (!int.TryParse(Console.ReadLine(), out id) || id < 0)
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter Project Meneger Id: ");
        }

        Console.WriteLine("1.NotStarted, 2.InProgress, 3.Completed");
        int choise;
        Console.Write("Enter Status:");
        while (!int.TryParse(Console.ReadLine(), out choise) || choise < 0 || choise > 4)
        {
            Console.WriteLine("Was entered in the wrong format .Try agin !");
            Console.Write("Enter Status :");
        }
        var status = new Status();
        if (choise == 1) status = Status.NotStarted;
        if (choise == 2) status = Status.InProgress;
        if (choise == 3) status = Status.Completed;


        try
        {
            var result = await _projectServise.PerformProject(id, status);
            var table = new Table();

            table.AddColumn("[slateblue1]Id[/]");
            table.AddColumn("[slateblue1]Name[/]");
            table.AddColumn("[slateblue1]Descreption[/]");
            table.AddColumn("[slateblue1]StartTime[/]");
            table.AddColumn("[slateblue1]EndTme[/]");
            table.AddColumn("[slateblue1]Status[/]");
            table.AddColumn("[slateblue1]ProjectMenegerId[/]");

            table.AddRow(result.Id.ToString(), result.Name, result.Description, result.StartTime.ToString(),
                result.EndTime.ToString(), result.Status.ToString(), result.ProjectMenegerId.ToString());
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

    public async void Delete()
    {
        Console.Clear();
        Console.Write("Enter project Id to delete :");
        int id;
        while (!int.TryParse(Console.ReadLine(), out id) || id < 0)
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter project Id to delete :");
        }
        try
        {
            _projectServise.DeleteAsync(id);
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
        Console.Write("Enter project Id :");
        int id;
        while (!int.TryParse(Console.ReadLine(), out id) || id < 0)
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter project Id :");
        }
        try
        {
            var result = await _projectServise.GetByIdAsync(id);
            var table = new Table();

            table.AddColumn("[slateblue1]Id[/]");
            table.AddColumn("[slateblue1]Name[/]");
            table.AddColumn("[slateblue1]Descreption[/]");
            table.AddColumn("[slateblue1]StartTime[/]");
            table.AddColumn("[slateblue1]EndTme[/]");
            table.AddColumn("[slateblue1]Status[/]");
            table.AddColumn("[slateblue1]ProjectMenegerId[/]");

            table.AddRow(result.Id.ToString(), result.Name, result.Description, result.StartTime.ToString(),
                result.EndTime.ToString(), result.Status.ToString(), result.ProjectMenegerId.ToString());
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
        table.AddColumn("[slateblue1]Name[/]");
        table.AddColumn("[slateblue1]Descreption[/]");
        table.AddColumn("[slateblue1]StartTime[/]");
        table.AddColumn("[slateblue1]EndTme[/]");
        table.AddColumn("[slateblue1]Status[/]");
        table.AddColumn("[slateblue1]ProjectMenegerId[/]");

        var project = await _projectServise.GetAllAsync();
        foreach (var result in project)
        {
            table.AddRow(result.Id.ToString(), result.Name, result.Description, result.StartTime.ToString(),
                result.EndTime.ToString(), result.Status.ToString(), result.ProjectMenegerId.ToString());
        }

        AnsiConsole.Write(table);
        Console.WriteLine("Enter any keyword to continue");
        Console.ReadKey();
        Console.Clear();
    }


    public async void Update()
    {
        Console.Clear();
        Console.Write("Enter ptoject Id to update :");
        long id;
        while (!long.TryParse(Console.ReadLine(), out id) || id < 0)
        {
            Console.WriteLine("Was entered in the wrong format .Try agin !");
            Console.Write("Enter project Id to update :");
        }
        Console.Write("Enter Name :");
        string name = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter Name :");
            name = Console.ReadLine();
        }

        Console.Write("Descreption :");
        string descreption = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(descreption))
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter Descreption :");
            descreption = Console.ReadLine();
        }

        Console.Write("Enter ProjectMeneger Id :");
        long projectMenegerId;
        while (!long.TryParse(Console.ReadLine(), out projectMenegerId) || projectMenegerId < 0)
        {
            Console.WriteLine("Was entered in the wrong format .Try agin !");
            Console.Write("Enter ProjectMeneger Id :");
        }

        Console.Write("Enter the project start time example year/month/day: ");
        DateTime startTime;
        while (!DateTime.TryParse(Console.ReadLine(), out startTime) && startTime < DateTime.UtcNow)
        {
            Console.WriteLine("Was entered in the wrong format .Try agin !");
            Console.Write("Please Enter the project start time example year/month/day: ");
        }

        Console.Write("Enter the project end time example year/month/day: ");
        DateTime endTime;
        while (!DateTime.TryParse(Console.ReadLine(), out endTime) && endTime < startTime)
        {
            Console.WriteLine("Was entered in the wrong format .Try agin !");
            Console.Write("Please Enter the project end time example year/month/day: ");
        }
        ProjectUpdateModel project = new ProjectUpdateModel()
        {

            Name = name,
            Description = descreption,
            EndTime = endTime,
            ProjectMenegerId = projectMenegerId,
            Status = Status.NotStarted,
        };

        try
        {
            var updateProject = await _projectServise.UpdateAsync(id, project);
            AnsiConsole.Markup("[orange3]Succesful updated[/]\n");
            var table = new Table();

            table.AddColumn("[slateblue1]Id[/]");
            table.AddColumn("[slateblue1]Name[/]");
            table.AddColumn("[slateblue1]Descreption[/]");
            table.AddColumn("[slateblue1]StartTime[/]");
            table.AddColumn("[slateblue1]EndTme[/]");
            table.AddColumn("[slateblue1]Status[/]");
            table.AddColumn("[slateblue1]ProjectMenegerId[/]");

            table.AddRow(updateProject.Id.ToString(), updateProject.Name, updateProject.Description, updateProject.StartTime.ToString(),
            updateProject.EndTime.ToString(), updateProject.Status.ToString(), updateProject.ProjectMenegerId.ToString());
            AnsiConsole.Write(table);
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

    public async void ViewProjectByProjectMenegerId()
    {
        Console.Clear();
        Console.Write("Enter project meneger id: ");
        long id;
        while (!long.TryParse(Console.ReadLine(), out id) || id < 0)
        {
            Console.WriteLine("Was entered in the wrong format .Try agin !");
            Console.Write("Enter project meneger id: ");
        }
        var table = new Table();

        table.AddColumn("[slateblue1]Id[/]");
        table.AddColumn("[slateblue1]Name[/]");
        table.AddColumn("[slateblue1]Descreption[/]");
        table.AddColumn("[slateblue1]StartTime[/]");
        table.AddColumn("[slateblue1]EndTme[/]");
        table.AddColumn("[slateblue1]Status[/]");
        table.AddColumn("[slateblue1]ProjectMenegerId[/]");

        var project = await _projectServise.ViewProjectByProjectMenegerIdAsync(id);
        foreach (var result in project)
        {
            table.AddRow(result.Id.ToString(), result.Name, result.Description, result.StartTime.ToString(),
                result.EndTime.ToString(), result.Status.ToString(), result.ProjectMenegerId.ToString());
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
        "Create", "Delete", "Update", "GetById" , "GetAll", "ViewAllUser","ViewAllTask", "Exit",
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
                case "ViewAllUser":
                    _userMenu.GetAll();
                    break;
                case "ViewAllTask":
                    _taskMenu.GetAll();
                    break;

                case "Exit": circle = false; break;
            }
        }
    }
}
