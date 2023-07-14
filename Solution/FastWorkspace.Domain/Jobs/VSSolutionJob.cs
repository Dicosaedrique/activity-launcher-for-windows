namespace FastWorkspace.Domain.Jobs;

public class VSSolutionJob : BaseJob
{
    public string SolutionFilePath { get; set; } = string.Empty;

    public override string GetScript()
    {
        if (!string.IsNullOrWhiteSpace(SolutionFilePath))
        {
            return $"Start-Process -FilePath \"{SolutionFilePath}\"";
        }

        return "# Invalid solution file path, you need to specify one!";
    }

    public override string GetDefaultName() => "Open Visual Studio Solution";
}
