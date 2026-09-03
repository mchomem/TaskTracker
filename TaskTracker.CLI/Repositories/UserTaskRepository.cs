namespace TaskTracker.CLI.Repositories;

public sealed class UserTaskRepository : IUserTaskRepository
{
    private readonly List<UserTask> _userTasks;
    private readonly string _fileName = $"{nameof(UserTask)}.json";
    private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
    {
        Converters = { new JsonStringEnumConverter() },
        PropertyNameCaseInsensitive = true
    };

    public UserTaskRepository()
    {
        CreateEmptyJsonFileIfNotExists();
        _userTasks = new List<UserTask>();
    }

    public async Task<IEnumerable<UserTask>> ListAllAsync(Expression<Func<UserTask, bool>> predicate)
    {
        await ReadAllAsync();

        return _userTasks.Where(predicate.Compile());
    }

    public async Task<UserTask> GetByIdAsync(long id)
    {
        await ReadAllAsync();

        return _userTasks.FirstOrDefault(t => t.Id == id)!;
    }

    public async Task AddAsync(UserTask userTask)
    {
        _userTasks.Add(userTask);
        await SaveChangesAsync();
    }

    public async Task UpdateAsync(UserTask userTask)
    {
        await ReadAllAsync();

        var index = _userTasks.FindIndex(t => t.Id == userTask.Id);
        _userTasks[index] = userTask;
        await SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        await ReadAllAsync();

        var userTask = _userTasks.FirstOrDefault(t => t.Id == id);
        _userTasks.Remove(userTask!);
        await SaveChangesAsync();
    }

    private async Task SaveChangesAsync()
    {
        var json = JsonSerializer.Serialize(_userTasks, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_fileName, json);
    }

    private async Task ReadAllAsync()
    {
        var json = await File.ReadAllTextAsync(_fileName);

        _userTasks.Clear();

        if (!string.IsNullOrWhiteSpace(json))
            _userTasks.AddRange(JsonSerializer.Deserialize<List<UserTask>>(json, _jsonSerializerOptions)!);
    }

    private void CreateEmptyJsonFileIfNotExists()
    {
        if (!File.Exists(_fileName))
        {
            File.Create(_fileName).Close();
            File.WriteAllText(_fileName, "[]");
        }
    }
}
