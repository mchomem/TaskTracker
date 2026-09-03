namespace TaskTracker.CLI.Exceptions;

public sealed class TaskAlreadyDoneException : Exception
{
    public TaskAlreadyDoneException(string message = "Cannot change the status of a done task.") : base(message) { }
}
