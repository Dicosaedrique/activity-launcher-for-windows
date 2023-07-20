using ActivityLauncher.Domain.Interfaces;

namespace ActivityLauncher.Domain.Model.Tasks;

public class ScriptTask : BaseTask, ICloneable<ScriptTask>
{
    public string Script { get; set; } = string.Empty;

    public override string? GetScript()
    {
        return Script;
    }

    public ScriptTask Clone()
    {
        return new ScriptTask()
        {
            Name = Name,
            Enabled = Enabled,
            Script = Script,
        };
    }
}
