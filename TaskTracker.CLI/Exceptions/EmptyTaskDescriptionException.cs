namespace TaskTracker.CLI.Exceptions;

public sealed class EmptyTaskDescriptionException : Exception
{
    public EmptyTaskDescriptionException(string message = "Description cannot be null or empty.") : base(message) { }
}
