namespace FastWorkspace.Domain.Jobs;

// todo: implement in the future
public class BrowserTabsJob : BaseJob
{
    public ICollection<string> Tabs { get; set; } = new List<string>();

    public override string GetScript()
    {
        return "# Sorry this feature is not available yet!";
    }
}
