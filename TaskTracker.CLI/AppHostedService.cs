namespace TaskTracker.CLI;

public sealed class AppHostedService : BackgroundService
{
    private readonly IUserTaskService _userTaskService;
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly string[] _args;

    public AppHostedService(IUserTaskService userTaskService,
        IHostApplicationLifetime hostApplicationLifetime,
        AppArguments appArguments)
    {
        _userTaskService = userTaskService;
        _hostApplicationLifetime = hostApplicationLifetime;
        _args = appArguments.Args;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await ProcessCommandAsync();
        Exit();
    }

    private async Task ProcessCommandAsync()
    {
        if (_args.Length == 0)
        {
            Exit();
            return;
        }

        var command = _args[0].ToLower();
        var value1 = _args.Length > 1 ? _args[1] : string.Empty;
        var value2 = _args.Length > 2 ? _args[2] : string.Empty;

        switch (command)
        {
            case "add":
                var userTask = await _userTaskService.AddAsync(value1);
                Console.WriteLine($"Task added successfully (ID: {userTask.Id})");
                break;

            case "update":
                await _userTaskService.UpdateAsync(value1, value2);
                break;

            case "mark-in-progress":
                await _userTaskService.ChangeStatusAsync(value1, UserTaskStatus.InProgress);
                break;

            case "mark-done":
                await _userTaskService.ChangeStatusAsync(value1, UserTaskStatus.Done);
                break;

            case "delete":
                await _userTaskService.DeleteAsync(Convert.ToInt64(value1));
                break;

            case "list":
                var tasks = Enumerable.Empty<UserTask>();

                switch (value1)
                {
                    case "":
                        tasks = await _userTaskService.ListAllAsync();
                        break;

                    case "done":
                        tasks = await _userTaskService.ListAllDoneAsync();
                        break;

                    case "todo":
                        tasks = await _userTaskService.ListAllTodoAsync();
                        break;

                    case "in-progress":
                        tasks = await _userTaskService.ListAllInProgressAsync();
                        break;

                    default:
                        Console.WriteLine("Invalid list argument. Use 'done', 'todo', or 'in-progress'.");
                        break;
                }

                if (!tasks.Any())
                {
                    Console.WriteLine("No tasks found.");
                    break;
                }

                foreach (var task in tasks)
                    Console.WriteLine($"ID: {task.Id} - Created At: {task.CreatedAt} - Description: {task.Description} - Status: {task.Status}");

                break;
        }
    }

    private void Exit()
    {
        _hostApplicationLifetime.StopApplication();
    }
}
