using ActivityLauncher.Domain.Interfaces;

namespace ActivityLauncher.Domain.Model.Jobs;

public class VSCodeJob : BaseJob, ICloneable<VSCodeJob>
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

    public VSCodeJob Clone()
    {
        return new VSCodeJob()
        {
            Name = Name,
            Enabled = Enabled,
            DirectoryPath = DirectoryPath,
        };
    }
}
