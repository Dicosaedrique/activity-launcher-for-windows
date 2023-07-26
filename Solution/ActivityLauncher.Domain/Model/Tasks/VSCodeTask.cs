using ActivityLauncher.Domain.Enums;
using ActivityLauncher.Domain.Interfaces;

namespace ActivityLauncher.Domain.Model.Tasks;

public class VSCodeTask : BaseTask, ICloneable<VSCodeTask>
{
    public override TaskType TaskType => TaskType.VSCodeTask;

    public string DirectoryPath { get; set; } = string.Empty;

    public override bool IsValid()
    {
        return true; // todo: check that DirectoryPath exists and is a directory
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
