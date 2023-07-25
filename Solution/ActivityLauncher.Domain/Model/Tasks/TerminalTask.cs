using System.Text;
using ActivityLauncher.Domain.Interfaces;
using ActivityLauncher.Domain.Model.Terminal;

namespace ActivityLauncher.Domain.Model.Tasks;

public class TerminalTask : BaseTask, ICloneable<TerminalTask>
{
    public List<TerminalTab> TerminalTabs { get; set; } = new();

    public override bool IsValid()
    {
        return TerminalTabs.TrueForAll(x => x.IsValid());
    }

    protected override string GetValidScript()
    {
        var builder = new StringBuilder();

        builder.Append("wt");

        for (var i = 0; i < TerminalTabs.Count; i++)
        {
            builder.Append(TerminalTabs[i].GetInvokeCommandArgs());

            if (i != TerminalTabs.Count - 1)
            {
                builder.Append(" `; new-tab");
            }
        }

        return builder.ToString();
    }

    public TerminalTask Clone()
    {
        return new TerminalTask()
        {
            Id = Id,
            Name = Name,
            CreationDate = CreationDate,
            TerminalTabs = TerminalTabs.Select(terminalTab => terminalTab.Clone()).ToList(),
        };
    }
}
