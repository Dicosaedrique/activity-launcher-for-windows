namespace FastWorkspace.Domain.Jobs;

// todo: in the future (DO NOT USE!!!)
public class BrowserTabsJob : BaseJob
{
    public ICollection<string> Tabs { get; set; } = new List<string>();

    public override string GetScript()
    {
        throw new NotImplementedException();
    }
}
