using ActivityLauncher.Domain.Interfaces;

namespace ActivityLauncher.Domain.Model.Tasks;

public class VSCodeTask : BaseTask, ICloneable<VSCodeTask>
{
    public string DirectoryPath { get; set; } = string.Empty;

    public override string? GetScript()
    {
        if (!string.IsNullOrWhiteSpace(DirectoryPath))
        {
            return $"code \"{DirectoryPath}\"";
        }

        return null;
    }

    public VSCodeTask Clone()
    {
        return new VSCodeTask()
        {
            Name = Name,
            Enabled = Enabled,
            DirectoryPath = DirectoryPath,
        };
    }
}
