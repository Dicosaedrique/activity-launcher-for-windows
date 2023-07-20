using ActivityLauncher.Domain.Interfaces;

namespace ActivityLauncher.Domain.Model.Jobs;

public class VSSolutionJob : BaseJob, ICloneable<VSSolutionJob>
{
    public string SolutionFilePath { get; set; } = string.Empty;

    public override string? GetScript()
    {
        if (!string.IsNullOrWhiteSpace(SolutionFilePath))
        {
            return $"Start-Process -FilePath \"{SolutionFilePath}\"";
        }

        return null;
    }

    public VSSolutionJob Clone()
    {
        return new VSSolutionJob()
        {
            Name = Name,
            Enabled = Enabled,
            SolutionFilePath = SolutionFilePath,
        };
    }
}
