using FastWorkspace.Domain.Common;
using FastWorkspace.Domain.Interfaces;

namespace FastWorkspace.Domain.Jobs;

public class ScriptJob : BaseJob
{
    public string Script { get; set; } = string.Empty;

    public override string GetScript()
    {
        return Script;
    }

    public override string GetDefaultName() => "Execute Custom PowerShell Script";

    public override IJob Clone()
    {
        return new ScriptJob()
        {
            Name = Name,
            Description = Description,
            Sequence = Sequence,
            Enabled = Enabled,
            Script = Script,
        };
    }
}
