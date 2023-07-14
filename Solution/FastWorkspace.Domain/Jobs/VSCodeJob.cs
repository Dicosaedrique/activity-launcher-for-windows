using FastWorkspace.Domain.Utils;

namespace FastWorkspace.Domain.Jobs;

public class VSCodeJob : BaseJob
{
    public string DirectoryPath { get; set; } = string.Empty;

    public override string GetScript()
    {
        if (PathHelper.IsValidPath(DirectoryPath))
        {
            return $"code \"{DirectoryPath}\"";
        }

        return string.Empty;
    }

    public override string GetDefaultName() => "Open Visual Studio Code Directory";
}
