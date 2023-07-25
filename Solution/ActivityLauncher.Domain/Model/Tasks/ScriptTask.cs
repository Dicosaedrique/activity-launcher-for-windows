using ActivityLauncher.Domain.Interfaces;

namespace ActivityLauncher.Domain.Model.Tasks;

public class ScriptTask : BaseTask, ICloneable<ScriptTask>
{
    public string Script { get; set; } = string.Empty;

    public override bool IsValid()
    {
        return true;
    }

    protected override string GetValidScript()
    {
        return Script;
    }

    public ScriptTask Clone()
    {
        return new ScriptTask()
        {
            Id = Id,
            Name = Name,
            CreationDate = CreationDate,
            Script = Script,
        };
    }
}
