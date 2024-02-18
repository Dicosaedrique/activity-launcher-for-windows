namespace ActivityLauncher.Client.Core.Events;

public class ApplicationEvent
{
    public ApplicationEventType Type { get; set; }

    public object? Data { get; set; }

    public ApplicationEvent(ApplicationEventType type, object? data = null)
    {
        Type = type;
        Data = data;
    }
}
