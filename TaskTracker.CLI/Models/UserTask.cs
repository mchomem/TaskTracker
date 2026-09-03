namespace TaskTracker.CLI.Models;

public sealed class UserTask
{
    [JsonConstructor]
    public UserTask(long id, string description, UserTaskStatus status, DateTime createdAt, DateTime? updateAt)
    {
        Id = id;
        Description = description;
        Status = status;
        CreatedAt = createdAt;
        UpdateAt = updateAt;
    }

    public UserTask(long id, string description)
    {
        Id = id;
        Description = description;
        Status = UserTaskStatus.Todo;
        CreatedAt = DateTime.Now;

        CheckInputs();
    }

    public long Id { get; private set; }
    public string Description { get; private set; }
    public UserTaskStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdateAt { get; private set; }

    public void Update(string description)
    {
        Description = description;
        UpdateAt = DateTime.Now;
    }

    public void ChangeStatus(UserTaskStatus status)
    {
        // Cannot change status from Done to Todo or InProgress.
        if (Status == UserTaskStatus.Done && (status == UserTaskStatus.Todo || status == UserTaskStatus.InProgress))
            throw new TaskAlreadyDoneException();

        Status = status;
        UpdateAt = DateTime.Now;
    }

    private void CheckInputs()
    {
        if(string.IsNullOrEmpty(Description))
            throw new EmptyTaskDescriptionException();
    }
}
