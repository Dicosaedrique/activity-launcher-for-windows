using System.Text;
using FastWorkspace.Domain.Interfaces;

namespace FastWorkspace.Domain.Common;

public class TerminalTab : ICloneable<TerminalTab>
{
    public string? Command { get; set; }

    public string? Title { get; set; }

    public string? LocationPath { get; set; }

    public string? Color { get; set; }

    public string GetInvokeCommandArgs()
    {
        var builder = new StringBuilder();

        if (!string.IsNullOrWhiteSpace(LocationPath))
        {
            builder.Append($" -d \"{LocationPath}\"");
        }

        if (!string.IsNullOrWhiteSpace(Title))
        {
            builder.Append($" --title \"{Title}\"");
        }

        if (!string.IsNullOrWhiteSpace(Color))
        {
            builder.Append($" --tabColor \"{Color}\"");
        }

        if (!string.IsNullOrWhiteSpace(Command))
        {
            builder.Append($" powershell -noExit \"{Command}\"");
        }

        return builder.ToString();
    }

    public TerminalTab Clone()
    {
        return new TerminalTab()
        {
            Command = Command,
            Title = Title,
            LocationPath = LocationPath,
            Color = Color,
        };
    }
}
