using FastWorkspace.Domain.Utils;

namespace FastWorkspace.Domain.Steps;

public class FolderStep : BaseStep
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
