using ActivityLauncher.Domain.Interfaces;

namespace ActivityLauncher.Domain.Model.Tasks;

public class VSSolutionTask : BaseTask, ICloneable<VSSolutionTask>
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

    public VSSolutionTask Clone()
    {
        return new VSSolutionTask()
        {
            Name = Name,
            Enabled = Enabled,
            SolutionFilePath = SolutionFilePath,
        };
    }
}
