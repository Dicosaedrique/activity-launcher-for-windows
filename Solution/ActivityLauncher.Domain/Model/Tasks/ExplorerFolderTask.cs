using ActivityLauncher.Domain.Enums;
using ActivityLauncher.Domain.Interfaces;

namespace ActivityLauncher.Domain.Model.Tasks;

public class ExplorerFolderTask : BaseTask, ICloneable<ExplorerFolderTask>
{
    public override TaskType TaskType => TaskType.ExplorerFolderTask;

    public string DirectoryPath { get; set; } = string.Empty;

    public override bool IsValid()
    {
        return true; // todo: validate that DirectoryPath is not null and is a *directory* that exists
    }

    protected override string GetValidScript()
    {
        return $"Start-Process -FilePath {DirectoryPath}";
    }

    public ExplorerFolderTask Clone()
    {
        return new ExplorerFolderTask()
        {
            Id = Id,
            Name = Name,
            CreationDate = CreationDate,
            DirectoryPath = DirectoryPath,
        };
    }
}
