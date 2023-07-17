using FastWorkspace.Domain.Interfaces;

namespace FastWorkspace.Domain.Jobs;

public class ProgramJob : BaseJob
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

    public override string GetDefaultName() => "Open Program";

    public override IJob Clone()
    {
        return new ProgramJob()
        {
            Name = Name,
            Description = Description,
            Sequence = Sequence,
            Enabled = Enabled,
            ProgramName = ProgramName,
        };
    }
}
