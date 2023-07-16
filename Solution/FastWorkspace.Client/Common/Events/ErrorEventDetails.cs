namespace FastWorkspace.Client.Common.Events;

public class ErrorEventDetails
{
    public string Title { get; }

    public string? Detail { get; }

    public Exception? Exception { get; }

    public ErrorEventDetails(string title, string? detail = null, Exception? exception = null)
    {
        Title = title;
        Detail = detail;
        Exception = exception;
    }
}

public static class ErrorEventDetailsExtensions
{
    public static ErrorEventDetails ToErrorEventDetails(this Exception response, string title)
    {
        return new ErrorEventDetails(title, response.Message, response);
    }
}