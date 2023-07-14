namespace FastWorkspace.Domain.Utils;

public class Result
{
    public bool Success => Exception == null;

    public bool Failure => !Success;

    public Exception? Exception { get; }

    public Result(Exception? exception = null)
    {
        Exception = exception;
    }

    public static Result SuccessResult => new Result();
}

public class ResultWithContent<TContent> : Result
{
    public TContent? Content { get; }

    public ResultWithContent(TContent content) : base()
    {
        Content = content;
    }

    public ResultWithContent(Exception exception) : base(exception)
    {
    }
}

public static class ResultExtensions
{
    public static Result AsResult(this Exception exception) => new Result(exception);

    public static ResultWithContent<T> AsResult<T>(this Exception exception) => new ResultWithContent<T>(exception);
}