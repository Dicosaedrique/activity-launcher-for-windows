namespace FastWorkspace.Domain.Steps;

// todo: in the future (DO NOT USE!!!)
public class BrowserTabs : BaseStep
{
    public ICollection<string> Tabs { get; set; } = new List<string>();

    public override string GetScript()
    {
        throw new NotImplementedException();
    }
}
