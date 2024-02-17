namespace ActivityLauncher.Client.Common.Events;

public class ErrorEventDetails
{
    public string Title { get; }

    public string? Detail => Exception?.Message;

    public Exception? Exception { get; }

    public ErrorEventDetails(string title, Exception? exception = null)
    {
        Title = title;
        Exception = exception;
    }
}

public static class ErrorEventDetailsExtensions
{
    public static ErrorEventDetails ToErrorEventDetails(this Exception exception, string title)
    {
        return new ErrorEventDetails(title, exception);
    }
}