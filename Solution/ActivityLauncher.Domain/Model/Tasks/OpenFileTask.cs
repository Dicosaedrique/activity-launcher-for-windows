using ActivityLauncher.Domain.Interfaces;

namespace ActivityLauncher.Domain.Model.Tasks;

public class OpenFileTask : BaseTask, ICloneable<OpenFileTask>
{
    public string FilePath { get; set; } = string.Empty;

    public override string? GetScript()
    {
        if (!string.IsNullOrWhiteSpace(FilePath))
        {
            return $"Start-Process -FilePath \"{FilePath}\"";
        }

        return null;
    }

    public OpenFileTask Clone()
    {
        return new OpenFileTask()
        {
            Name = Name,
            Enabled = Enabled,
            FilePath = FilePath,
        };
    }
}
