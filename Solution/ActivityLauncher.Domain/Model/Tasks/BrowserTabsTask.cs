using ActivityLauncher.Domain.Interfaces;

namespace ActivityLauncher.Domain.Model.Tasks;

// todo: implement in the future
public class BrowserTabsTask : BaseTask, ICloneable<BrowserTabsTask>
{
    public ICollection<string> Tabs { get; set; } = new List<string>();

    public override string? GetScript()
    {
        return "# Sorry this feature is not available yet!";
    }

    public BrowserTabsTask Clone()
    {
        return new BrowserTabsTask()
        {
            Name = Name,
            Enabled = Enabled,
            Tabs = new List<string>(Tabs),
        };
    }
}
