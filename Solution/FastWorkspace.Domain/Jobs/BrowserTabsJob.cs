using FastWorkspace.Domain.Interfaces;

namespace FastWorkspace.Domain.Jobs;

// todo: implement in the future
public class BrowserTabsJob : BaseJob
{
    public ICollection<string> Tabs { get; set; } = new List<string>();

    public override string GetScript()
    {
        return "# Sorry this feature is not available yet!";
    }

    public override string GetDefaultName() => "Open Tabs In Browser";

    public override IJob Clone()
    {
        return new BrowserTabsJob()
        {
            Name = Name,
            Description = Description,
            Sequence = Sequence,
            Enabled = Enabled,
            Tabs = new List<string>(Tabs),
        };
    }
}
