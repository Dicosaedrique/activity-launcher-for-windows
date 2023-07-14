using System.Text;
using FastWorkspace.Domain.Common;

namespace FastWorkspace.Domain.Jobs;

public class TerminalJob : BaseJob
{
    public TerminalTab TerminalTab { get; set; } = new();

    public override string GetScript()
    {
        var builder = new StringBuilder();

        builder.Append("wt");

        builder.Append(TerminalTab.GetInvokeCommandArgs());

        return builder.ToString();
    }

    public override string GetDefaultName() => "Open Windows Terminal";
}
