using System.Text;
using FastWorkspace.Domain.Common;
using FastWorkspace.Domain.Interfaces;

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

    public override IJob Clone()
    {
        return new TerminalJob()
        {
            Name = Name,
            Description = Description,
            Sequence = Sequence,
            Enabled = Enabled,
            TerminalTab = TerminalTab.Clone(),
        };
    }
}
