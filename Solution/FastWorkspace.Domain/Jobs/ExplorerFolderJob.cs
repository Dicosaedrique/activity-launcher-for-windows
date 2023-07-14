namespace FastWorkspace.Domain.Jobs;

public class ExplorerFolderJob : BaseJob
{
    public string DirectoryPath { get; set; } = string.Empty;

    public override string GetScript()
    {
        if (!string.IsNullOrWhiteSpace(DirectoryPath))
        {
            return $"Start-Process -FilePath {DirectoryPath}";
        }

        return "# Invalid directory path, you need to specify one!";
    }

    public override string GetDefaultName() => "Open Explorer Folder";
}
