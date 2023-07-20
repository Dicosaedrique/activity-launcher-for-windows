using FastWorkspace.Domain.Interfaces;

namespace FastWorkspace.Domain.Model.Jobs;

public class ExplorerFolderJob : BaseJob, ICloneable<ExplorerFolderJob>
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

    public ExplorerFolderJob Clone()
    {
        return new ExplorerFolderJob()
        {
            Name = Name,
            Enabled = Enabled,
            DirectoryPath = DirectoryPath,
        };
    }
}
