// Specification to implement this app: https://roadmap.sh/projects/task-tracker

var builder = Host.CreateApplicationBuilder();
builder.Logging.ClearProviders();
builder.Services.AddSingleton(new AppArguments() { Args = args });
builder.Services.AddScoped<IUserTaskService, UserTaskService>();
builder.Services.AddScoped<IUserTaskRepository, UserTaskRepository>();
builder.Services.AddHostedService<AppHostedService>();

var host = builder.Build();
await host.RunAsync();
