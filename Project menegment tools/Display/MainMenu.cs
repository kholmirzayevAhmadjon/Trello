using Project_menegment_tools.Enums;
using Project_menegment_tools.Services;
using Spectre.Console;
using System.Net.WebSockets;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace Project_menegment_tools.Display;

public class MainMenu
{
    private readonly UserMenu userMenu;
    private readonly ProjectMenu projectMenu;
    private readonly TaskMenu taskMenu;
    private readonly UserService userService;
    private readonly ProjectServise projectServise;
    private readonly TasksService tasksService;

    public MainMenu()
    {
        userService = new UserService();
        projectServise = new ProjectServise();
        tasksService = new TasksService();

        userMenu = new UserMenu(userService);
        taskMenu = new TaskMenu(tasksService, userService, projectServise);
        projectMenu = new ProjectMenu(projectServise, userService, userMenu, taskMenu);
    }

    public async void Main()
    {
        key1:
        bool circle = true;
        while (circle)
        {
            Console.Clear();
            var category = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("[darkorange3_1]-- Welcome to trello --[/]")
            .PageSize(10)
            .AddChoices(new[] {
              "Sign in", "Sign up", "Exit"
            }));
            switch (category)
            {
                case "Sign in":
                    {
                        Console.Write("Enter Email (email@gmail.com):");
                        string email = Console.ReadLine();
                        while (!Regex.IsMatch(email, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]{3,}$"))
                        {
                            Console.WriteLine("Was entered in the wrong format .Try again !");
                            Console.Write("Enter Email (email@gmail.com):");
                            email = Console.ReadLine();
                        }

                        string password = AnsiConsole.Prompt(
                       new TextPrompt<string>("Enter password :")
                        .PromptStyle("red")
                        .Secret());


                        Console.WriteLine("1.Admin, 2.ProjectManager, 3.TeamMember");
                        int choice;
                        Console.Write("Enter Role:");
                        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 3)
                        {
                            Console.WriteLine("Was entered in the wrong format .Try agin !");
                            Console.Write("Enter Role :");
                        }
                        Role role = new Role();
                        if (choice == 1) role = Role.Admin;
                        if (choice == 2) role = Role.ProjectManager;
                        if (choice == 3) role = Role.TeamMember;

                        var result = userService.GetByEmailPassword(email, password, role).Result;

                        if(result.Equals( true))
                        {
                            if (role == Role.Admin)
                            {
                                bool res = true;
                                while (res)
                                {
                                    Console.Clear();
                                    var table = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                    .Title("[darkorange3_1]-- Welcome to trello --[/]")
                                    .PageSize(10)
                                    .AddChoices(new[] {
                                       "UserMenu", "ProjectMenu","DeleteUser", "Exit"
                                    }));
                                    switch (table)
                                    {
                                        case "UserMenu":
                                            userMenu.Display();
                                            break;
                                        case "ProjectMenu":
                                            projectMenu.Display();
                                            break;
                                        case "DeleteUser":
                                            userMenu.Delete();
                                            break;
                                        case "Exit":
                                            goto key1;
                                    }
                                }
                            }
                            if (role == Role.ProjectManager)
                            {
                                bool ex = true;
                                while (ex)
                                {
                                    Console.Clear();
                                    var table = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                    .Title("[darkorange3_1]-- Welcome to trello --[/]")
                                    .PageSize(10)
                                    .AddChoices(new[] {
                                       "UserMenu", "TaskMenu", "ViewProject", "PerformProject", "Exit"
                                    }));
                                    switch (table)
                                    {
                                        case "UserMenu":
                                            userMenu.Display();
                                            break;
                                        case "TaskMenu":
                                            taskMenu.Display();
                                            break;
                                        case "ViewProject":
                                            projectMenu.ViewProjectByProjectMenegerId();
                                            break;
                                        case "PerformProject":
                                            projectMenu.PerformProject();
                                            break;
                                        case "Exit":
                                            goto key1;
                                    }
                                }
                            }

                            if (role == Role.TeamMember)
                            {
                                bool ex = true;
                                while (ex)
                                {
                                    Console.Clear();
                                    var table = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                    .Title("[darkorange3_1]-- Welcome to trello --[/]")
                                    .PageSize(10)
                                    .AddChoices(new[] {
                                       "ViewTask", "PerformTask", "UpdateUser", "Exit"
                                    }));
                                    switch (table)
                                    {
                                        case "ViewTask":
                                            taskMenu.ViewTaskViewTaskByTeamMemberId();
                                            break;
                                        case "PerformTask":
                                            taskMenu.PerformTask();
                                            break;
                                        case "UpdateUser":
                                            userMenu.Update();
                                            break;
                                        case "Exit":
                                            goto key1;
                                    }
                                }
                            }
                        }

                        else
                        {
                            await Console.Out.WriteLineAsync("You are not registered, please register");
                            Thread.Sleep(3000);
                            goto key;
                        }
                    }
                    break;
                case "Sign up":
                    key:
                    userMenu.Create();
                    Thread.Sleep(3000);
                    break;
                case "Exit":
                    circle = false;
                    break;
            }
        }
    }
}
