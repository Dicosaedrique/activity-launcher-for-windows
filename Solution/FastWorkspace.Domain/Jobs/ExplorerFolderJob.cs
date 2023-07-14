using FastWorkspace.Domain.Utils;

namespace FastWorkspace.Domain.Jobs;

public class ExplorerFolderJob : BaseJob
{
    public string FolderPath { get; set; } = string.Empty;

    public override string GetScript()
    {
        if (PathHelper.IsValidPath(FolderPath))
        {
            return $"Start-Process -FilePath {PathHelper.SanatizePath(FolderPath)}";
        }

        return string.Empty;
    }
}
