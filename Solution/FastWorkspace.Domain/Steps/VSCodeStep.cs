using FastWorkspace.Domain.Utils;

namespace FastWorkspace.Domain.Steps;

public class VSCodeStep : BaseStep
{
    public string FolderPath { get; set; } = string.Empty;

    public override string GetScript()
    {
        if (PathHelper.IsValidPath(FolderPath))
        {
            return $"code \"{FolderPath}\"";
        }

        return string.Empty;
    }
}
