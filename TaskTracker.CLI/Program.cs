var services = new ServiceCollection();
services.AddScoped<IUserTaskService, UserTaskService>();
services.AddScoped<IUserTaskRepository, UserTaskRepository>();
services.AddScoped<CommandProcessor>();

var provider = services.BuildServiceProvider();

try
{
    var commandProcessor = provider.GetRequiredService<CommandProcessor>();
    await commandProcessor.ProcessCommandAsync(args);
}
catch (Exception ex)
{
    await Console.Error.WriteLineAsync($"Error: {ex.Message}");
    Environment.Exit(1);
}
