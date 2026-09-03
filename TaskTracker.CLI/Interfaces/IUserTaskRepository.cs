namespace TaskTracker.CLI.Interfaces;

public interface IUserTaskRepository
{
    public Task<IEnumerable<UserTask>> ListAllAsync(Expression<Func<UserTask, bool>> predicate);
    public Task<UserTask> GetByIdAsync(long id);
    public Task AddAsync(UserTask userTask);
    public Task UpdateAsync(UserTask userTask);
    public Task DeleteAsync(long id);
}
