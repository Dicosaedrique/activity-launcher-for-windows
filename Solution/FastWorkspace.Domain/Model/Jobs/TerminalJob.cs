using System.Text;
using FastWorkspace.Domain.Interfaces;
using FastWorkspace.Domain.Model.Terminal;

namespace FastWorkspace.Domain.Model.Jobs;

public class TerminalJob : BaseJob, ICloneable<TerminalJob>
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

    public TerminalJob Clone()
    {
        return new TerminalJob()
        {
            Name = Name,
            Enabled = Enabled,
            TerminalTabs = TerminalTabs.Select(terminalTab => terminalTab.Clone()).ToList(),
        };
    }
}
