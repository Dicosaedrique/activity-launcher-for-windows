using ActivityLauncher.Domain.Common.Utils;
using ActivityLauncher.Domain.Enums;
using ActivityLauncher.Domain.Interfaces;

namespace ActivityLauncher.Domain.Model.Tasks;

public class VSCodeTask : BaseTask, ICloneable<VSCodeTask>
{
    public override TaskType TaskType => TaskType.VSCodeTask;

    public string DirectoryPath { get; set; } = string.Empty;

    public override bool IsValid()
    {
        return ValidationHelper.IsValidDirectory(DirectoryPath);
    }

    protected override string GetValidScript()
    {
        return $"code \"{DirectoryPath}\"";
    }

    public VSCodeTask Clone()
    {
        return new VSCodeTask()
        {
            Id = Id,
            Name = Name,
            CreationDate = CreationDate,
            DirectoryPath = DirectoryPath,
        };
    }
}
