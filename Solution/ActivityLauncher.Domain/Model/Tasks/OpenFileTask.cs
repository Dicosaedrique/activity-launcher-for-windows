using ActivityLauncher.Domain.Interfaces;

namespace ActivityLauncher.Domain.Model.Tasks;

public class OpenFileTask : BaseTask, ICloneable<OpenFileTask>
{
    public string FilePath { get; set; } = string.Empty;

    public override bool IsValid()
    {
        return true; // todo: validate that FilePath is not null and is a *file* that exists
    }

    protected override string GetValidScript()
    {
        return $"Start-Process -FilePath \"{FilePath}\"";
    }

    public OpenFileTask Clone()
    {
        return new OpenFileTask()
        {
            Id = Id,
            Name = Name,
            CreationDate = CreationDate,
            FilePath = FilePath,
        };
    }
}
