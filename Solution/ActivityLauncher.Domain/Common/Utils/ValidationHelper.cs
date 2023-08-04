using System.Text.RegularExpressions;

namespace ActivityLauncher.Domain.Common.Utils;

public static class ValidationHelper
{
    private static readonly Regex HexaDecimalRegex = new Regex("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$");
    private static readonly Regex TerminalTabTitleRegex = new Regex("^[^\\\\\"\\n\\r]*$");

    public static bool IsValidDirectory(string directoryPath)
    {
        return !string.IsNullOrWhiteSpace(directoryPath) && Directory.Exists(directoryPath);
    }

    public static bool IsValidFile(string filePath)
    {
        return !string.IsNullOrWhiteSpace(filePath) && File.Exists(filePath);
    }

    public static bool IsValidHexaColor(string hexaColor)
    {
        return HexaDecimalRegex.IsMatch(hexaColor);
    }

    public static bool IsValidTerminalTabTitle(string terminalTabTitle)
    {
        return TerminalTabTitleRegex.IsMatch(terminalTabTitle);
    }
}
