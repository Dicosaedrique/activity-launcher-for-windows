using FastWorkspace.Domain.Utils;

namespace FastWorkspace.Domain.Jobs;

public class ExplorerFolderJob : BaseJob
{
    public string DirectoryPath { get; set; } = string.Empty;

    public override string GetScript()
    {
        if (PathHelper.IsValidPath(DirectoryPath))
        {
            return $"Start-Process -FilePath {PathHelper.SanatizePath(DirectoryPath)}";
        }

        return string.Empty;
    }

    public override string GetDefaultName() => "Open Explorer Folder";
}
