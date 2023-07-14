using FastWorkspace.Domain.Common;
using System.Text;

namespace FastWorkspace.Domain.Steps;

public class TerminalStep : BaseStep
{
    public TerminalTab TerminalTab { get; set; } = new();

    public override string GetScript()
    {
        var builder = new StringBuilder();

        builder.Append("wt");

        builder.Append(TerminalTab.GetInvokeCommandArgs());

        return builder.ToString();
    }
}
