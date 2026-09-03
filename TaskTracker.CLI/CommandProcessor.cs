namespace TaskTracker.CLI;

public sealed class CommandProcessor
{
    private readonly IUserTaskService _userTaskService;

    public CommandProcessor(IUserTaskService userTaskService)
    {
        _userTaskService = userTaskService;
    }

    public async Task ProcessCommandAsync(string[] args)
    {
        if (args.Length == 0)
            return;

        var command = args[0].ToLower();
        var value1 = args.Length > 1 ? args[1] : string.Empty;
        var value2 = args.Length > 2 ? args[2] : string.Empty;

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
                        Console.WriteLine("Invalid list argument. Use 'done', 'todo', or 'in-progress' with 'list'.");
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

            default:
                Console.WriteLine("Invalid command. Use 'add', 'update', 'mark-in-progress', 'mark-done', 'delete', or 'list'.");
                break;
        }
    }
}
