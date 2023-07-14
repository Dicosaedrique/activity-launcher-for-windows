using System.Text;
using FastWorkspace.Domain.Utils;

namespace FastWorkspace.Domain.Jobs;

public class ProgramFileJob : BaseJob
{
    public string ProgramFilePath { get; set; } = string.Empty;

    public string ArgumentList { get; set; } = string.Empty;

    public override string GetScript()
    {
        var builder = new StringBuilder();

        if (PathHelper.IsValidPath(ProgramFilePath))
        {
            builder.Append($"Start-Process -FilePath \"{PathHelper.SanatizePath(ProgramFilePath)}\"");
        }

        if (!string.IsNullOrWhiteSpace(ArgumentList))
        {
            builder.Append($" -ArgumentList \"{ArgumentList}\"");
        }

        return builder.ToString();
    }
}
