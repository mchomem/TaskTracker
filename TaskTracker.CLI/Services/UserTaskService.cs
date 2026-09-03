namespace TaskTracker.CLI.Services;

public sealed class UserTaskService : IUserTaskService
{
    private readonly IUserTaskRepository _userTaskRepository;

    public UserTaskService(IUserTaskRepository userTaskRepository)
    {
        _userTaskRepository = userTaskRepository;
    }

    public async Task<UserTask> AddAsync(string description)
    {
        var id = await GetNextValueAsync();
        var userTask = new UserTask(id, description);
        await _userTaskRepository.AddAsync(userTask);
        return userTask;
    }

    public async Task DeleteAsync(long id)
    {
        await _userTaskRepository.DeleteAsync(id);
    }

    public async Task<UserTask> GetByIdAsync(long id)
    {
        var result = await _userTaskRepository.GetByIdAsync(id);
        return result;
    }

    public async Task<IEnumerable<UserTask>> ListAllAsync()
    {
        Expression<Func<UserTask, bool>> predicate = x => true;
        var result = await _userTaskRepository.ListAllAsync(predicate);
        return result;
    }

    public async Task<IEnumerable<UserTask>> ListAllDoneAsync()
    {
        Expression<Func<UserTask, bool>> predicate = x => x.Status == UserTaskStatus.Done;
        var result = await _userTaskRepository.ListAllAsync(predicate);
        return result;
    }

    public async Task<IEnumerable<UserTask>> ListAllInProgressAsync()
    {
        Expression<Func<UserTask, bool>> predicate = x => x.Status == UserTaskStatus.InProgress;
        var result = await _userTaskRepository.ListAllAsync(predicate);
        return result;
    }

    public async Task<IEnumerable<UserTask>> ListAllTodoAsync()
    {
        Expression<Func<UserTask, bool>> predicate = x => x.Status == UserTaskStatus.Todo;
        var result = await _userTaskRepository.ListAllAsync(predicate);
        return result;
    }

    public async Task UpdateAsync(string id, string description)
    {
        var userTaskToUpdate = await _userTaskRepository.GetByIdAsync(Convert.ToInt64(id));
        userTaskToUpdate.Update(description);
        await _userTaskRepository.UpdateAsync(userTaskToUpdate);
    }

    public async Task ChangeStatusAsync(string id, UserTaskStatus status)
    {
        var userTaskToUpdate = await _userTaskRepository.GetByIdAsync(Convert.ToInt64(id));
        userTaskToUpdate.ChangeStatus(status);
        await _userTaskRepository.UpdateAsync(userTaskToUpdate);
    }

    public async Task<long> GetNextValueAsync()
    {
        Expression<Func<UserTask, bool>> predicate = x => true;
        var records = await _userTaskRepository.ListAllAsync(predicate);
        var id = records.Any() ? records.Max(t => t.Id) + 1 : 1;
        return id;
    }
}
