using FastWorkspace.Domain.Interfaces;

namespace FastWorkspace.Domain.Jobs;

public class VSCodeJob : BaseJob
{
    public string DirectoryPath { get; set; } = string.Empty;

    public override string GetScript()
    {
        if (!string.IsNullOrWhiteSpace(DirectoryPath))
        {
            return $"code \"{DirectoryPath}\"";
        }

        return "# Invalid directory path, you need to specify one!";
    }

    public override string GetDefaultName() => "Open Visual Studio Code Directory";

    public override IJob Clone()
    {
        return new VSCodeJob()
        {
            Name = Name,
            Description = Description,
            Sequence = Sequence,
            Enabled = Enabled,
            DirectoryPath = DirectoryPath,
        };
    }
}
