using ActivityLauncher.Domain.Interfaces;

namespace ActivityLauncher.Domain.Model.Jobs;

// todo: implement in the future
public class BrowserTabsJob : BaseJob, ICloneable<BrowserTabsJob>
{
    public ICollection<string> Tabs { get; set; } = new List<string>();

    public override string? GetScript()
    {
        return "# Sorry this feature is not available yet!";
    }

    public BrowserTabsJob Clone()
    {
        return new BrowserTabsJob()
        {
            Name = Name,
            Enabled = Enabled,
            Tabs = new List<string>(Tabs),
        };
    }
}
