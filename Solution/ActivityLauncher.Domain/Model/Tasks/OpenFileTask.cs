using ActivityLauncher.Domain.Common.Utils;
using ActivityLauncher.Domain.Enums;
using ActivityLauncher.Domain.Interfaces;

namespace ActivityLauncher.Domain.Model.Tasks;

public class OpenFileTask : BaseTask, ICloneable<OpenFileTask>
{
    public override TaskType TaskType => TaskType.OpenFileTask;

    public string FilePath { get; set; } = string.Empty;

    public override bool IsValid()
    {
        return ValidationHelper.IsValidFile(FilePath);
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
