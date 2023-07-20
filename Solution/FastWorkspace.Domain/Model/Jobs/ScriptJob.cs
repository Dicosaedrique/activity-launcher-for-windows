using FastWorkspace.Domain.Interfaces;

namespace FastWorkspace.Domain.Model.Jobs;

public class ScriptJob : BaseJob, ICloneable<ScriptJob>
{
    public string Script { get; set; } = string.Empty;

    public override string? GetScript()
    {
        return Script;
    }

    public ScriptJob Clone()
    {
        return new ScriptJob()
        {
            Name = Name,
            Enabled = Enabled,
            Script = Script,
        };
    }
}
