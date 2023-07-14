namespace FastWorkspace.Domain.Steps;

public class ProgramStep : BaseStep
{
    public string ProgramName { get; set; } = string.Empty;

    public override string GetScript()
    {
        if (!string.IsNullOrWhiteSpace(ProgramName))
        {
            return $"Start-Process {ProgramName}";
        }

        return string.Empty;
    }
}
