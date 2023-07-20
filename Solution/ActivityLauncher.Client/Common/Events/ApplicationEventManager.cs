namespace ActivityLauncher.Client.Common.Events;

public class ApplicationEventManager
{
    public delegate Task ApplicationEventHandler(object sender, ApplicationEvent applicationEvent);

    public event ApplicationEventHandler? OnEvent;

    public async Task PublishEvent(object sender, ApplicationEvent applicationEvent)
    {
        if (OnEvent == null) return;
        await OnEvent(sender, applicationEvent);
    }

    public async Task PublishEvent(object sender, ApplicationEventType eventType, object? data = null)
    {
        await PublishEvent(sender, new ApplicationEvent(eventType, data));
    }

    public async Task PublishError(object sender, ErrorEventDetails details)
    {
        await PublishEvent(sender, ApplicationEventType.ErrorOccurred, details);
    }
}
