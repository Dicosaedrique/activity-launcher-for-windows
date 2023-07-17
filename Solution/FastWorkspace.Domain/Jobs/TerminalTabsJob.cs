using System.Text;
using FastWorkspace.Domain.Common;
using FastWorkspace.Domain.Interfaces;

namespace FastWorkspace.Domain.Jobs;

public class TerminalTabsJob : BaseJob
{
    public ICollection<TerminalTab> TerminalTabs { get; set; } = new List<TerminalTab>();

    public override string GetScript()
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

    public override string GetDefaultName() => "Open Windows Terminal Tabs";

    public override IJob Clone()
    {
        return new TerminalTabsJob()
        {
            Name = Name,
            Description = Description,
            Sequence = Sequence,
            Enabled = Enabled,
            TerminalTabs = TerminalTabs.Select(terminalTab => terminalTab.Clone()).ToList(),
        };
    }
}
