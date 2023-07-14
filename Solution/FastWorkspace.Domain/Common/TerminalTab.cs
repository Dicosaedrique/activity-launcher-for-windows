using System.Text;
using FastWorkspace.Domain.Utils;

namespace FastWorkspace.Domain.Common;

public class TerminalTab
{
    public string Command { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string LocationPath { get; set; } = string.Empty;

    public string Color { get; set; } = string.Empty;

    public string GetInvokeCommandArgs()
    {
        var builder = new StringBuilder();

        if (PathHelper.IsValidPath(LocationPath))
        {
            builder.Append($" -d \"{PathHelper.SanatizePath(LocationPath)}\"");
        }

        if (!string.IsNullOrWhiteSpace(Title))
        {
            builder.Append($" --title \"{StringHelper.SanatizeString(Title)}\"");
        }

        if (!string.IsNullOrWhiteSpace(Color))
        {
            builder.Append($" --tabColor \"{StringHelper.SanatizeString(Color)}\"");
        }

        if (!string.IsNullOrWhiteSpace(Command))
        {
            builder.Append($" powershell -noExit \"{StringHelper.SanatizeString(Command)}\"");
        }

        return builder.ToString();
    }
}
