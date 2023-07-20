using System.Text;
using ActivityLauncher.Domain.Interfaces;
using ActivityLauncher.Domain.Model.Terminal;

namespace ActivityLauncher.Domain.Model.Tasks;

public class TerminalTask : BaseTask, ICloneable<TerminalTask>
{
    public List<TerminalTab> TerminalTabs { get; set; } = new();

    public override string? GetScript()
    {
        var builder = new StringBuilder();

        builder.Append("wt");

        for (var i = 0; i < TerminalTabs.Count; i++)
        {
            var terminalTab = TerminalTabs.ElementAt(i);

            builder.Append(terminalTab.GetInvokeCommandArgs());

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
            Name = Name,
            Enabled = Enabled,
            TerminalTabs = TerminalTabs.Select(terminalTab => terminalTab.Clone()).ToList(),
        };
    }
}
