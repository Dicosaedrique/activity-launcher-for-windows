using FastWorkspace.Domain.Common;
using System.Text;

namespace FastWorkspace.Domain.Steps;

public class TerminalTabsStep : BaseStep
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
                builder.Append(" ``; new-tab");
            }
        }

        return builder.ToString();
    }
}
