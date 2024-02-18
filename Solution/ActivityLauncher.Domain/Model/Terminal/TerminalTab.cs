using System.Text;
using ActivityLauncher.Domain.Common.Utils;
using ActivityLauncher.Domain.Interfaces;

namespace ActivityLauncher.Domain.Model.Terminal;

public class TerminalTab : ICloneable<TerminalTab>
{
    public string? Title { get; set; }

    public string? LocationPath { get; set; }

    public string? Command { get; set; }

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

    public bool IsValid()
    {
        var valid = true;

        if (!string.IsNullOrWhiteSpace(LocationPath) && !ValidationHelper.IsValidDirectory(LocationPath))
        {
            valid = false;
        }

        if (!string.IsNullOrWhiteSpace(Color) && !ValidationHelper.IsValidHexaColor(Color))
        {
            valid = false;
        }

        if (!string.IsNullOrWhiteSpace(Title) && !ValidationHelper.IsValidTerminalTabTitle(Title))
        {
            valid = false;
        }

        return valid;
    }

    public TerminalTab Clone()
    {
        return new TerminalTab()
        {
            Title = Title,
            LocationPath = LocationPath,
            Command = Command,
            Color = Color,
        };
    }
}
