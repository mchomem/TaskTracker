namespace TaskTracker.CLI.Interfaces;

public interface IUserTaskService
{
    Task<UserTask> AddAsync(string description);
    Task UpdateAsync(string id, string description);
    Task ChangeStatusAsync(string id, UserTaskStatus status);
    Task DeleteAsync(long id);
    Task<IEnumerable<UserTask>> ListAllAsync();
    Task<IEnumerable<UserTask>> ListAllDoneAsync();
    Task<IEnumerable<UserTask>> ListAllTodoAsync();
    Task<IEnumerable<UserTask>> ListAllInProgressAsync();
    Task<UserTask> GetByIdAsync(long id);
    Task<long> GetNextValueAsync();
}
