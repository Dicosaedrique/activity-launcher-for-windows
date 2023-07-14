using System.Text;

namespace FastWorkspace.Domain.Jobs;

public class ProgramFileJob : BaseJob
{
    public string ProgramFilePath { get; set; } = string.Empty;

    public string ArgumentList { get; set; } = string.Empty;

    public override string GetScript()
    {
        var builder = new StringBuilder();

        if (!string.IsNullOrWhiteSpace(ProgramFilePath))
        {
            builder.Append($"Start-Process -FilePath \"{ProgramFilePath}\"");

            if (!string.IsNullOrWhiteSpace(ArgumentList))
            {
                builder.Append($" -ArgumentList \"{ArgumentList}\"");
            }
        }
        else
        {
            builder.Append("# Invalid program file path, you need to specify one!");
        }

        return builder.ToString();
    }

    public override string GetDefaultName() => "Open Program File";
}
