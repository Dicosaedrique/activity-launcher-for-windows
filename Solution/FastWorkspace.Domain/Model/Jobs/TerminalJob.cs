using System.Text;
using FastWorkspace.Domain.Interfaces;
using FastWorkspace.Domain.Model.Terminal;

namespace FastWorkspace.Domain.Model.Jobs;

public class TerminalJob : BaseJob, ICloneable<TerminalJob>
{
    public TerminalTab TerminalTab { get; set; } = new();

    public override string? GetScript()
    {
        var builder = new StringBuilder();

        builder.Append("wt");

        builder.Append(TerminalTab.GetInvokeCommandArgs());

        return builder.ToString();
    }

    public TerminalJob Clone()
    {
        return new TerminalJob()
        {
            Name = Name,
            Enabled = Enabled,
            TerminalTab = TerminalTab.Clone(),
        };
    }
}
