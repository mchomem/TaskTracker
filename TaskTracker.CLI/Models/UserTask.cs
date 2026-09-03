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
        CheckInputs(description);

        Id = id;
        Description = description.Trim();
        Status = UserTaskStatus.Todo;
        CreatedAt = DateTime.Now;
    }

    public long Id { get; private set; }
    public string Description { get; private set; }
    public UserTaskStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdateAt { get; private set; }

    public void Update(string description)
    {        
        CheckInputs(description);

        Description = description.Trim();
        UpdateAt = DateTime.Now;
    }

    public void ChangeStatus(UserTaskStatus status)
    {
        // Cannot change status from Done to To-do or InProgress.
        if (Status == UserTaskStatus.Done)
            throw new TaskAlreadyDoneException();

        Status = status;
        UpdateAt = DateTime.Now;
    }

    private void CheckInputs(string description)
    {
        if(string.IsNullOrEmpty(description))
            throw new EmptyTaskDescriptionException();
    }
}
