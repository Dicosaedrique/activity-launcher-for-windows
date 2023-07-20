using ActivityLauncher.Domain.Interfaces;

namespace ActivityLauncher.Domain.Model.Tasks;

public class ExplorerFolderTask : BaseTask, ICloneable<ExplorerFolderTask>
{
    public string DirectoryPath { get; set; } = string.Empty;

    public override string? GetScript()
    {
        if (!string.IsNullOrWhiteSpace(DirectoryPath))
        {
            return $"Start-Process -FilePath {DirectoryPath}";
        }

        return null;
    }

    public ExplorerFolderTask Clone()
    {
        return new ExplorerFolderTask()
        {
            Name = Name,
            Enabled = Enabled,
            DirectoryPath = DirectoryPath,
        };
    }
}
