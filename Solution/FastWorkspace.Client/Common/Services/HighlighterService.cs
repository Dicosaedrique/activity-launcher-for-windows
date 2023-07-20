using System.Text;
using FastWorkspace.Domain.Enums;
using FastWorkspace.Domain.Model;
using FastWorkspace.Domain.Services.Declarations;
using Microsoft.AspNetCore.Components;

namespace FastWorkspace.Client.Common.Services;

public class HighlighterService
{
    private const string template = "<code style=\"font-size: 1rem;\" class=\"{0}\">{1}</code>";

    private const string LightClass = "hljs-light";
    private const string DarkClass = "hljs-dark";

    private readonly InteropService _interopService;

    private readonly IAppConfiguration _appConfiguration;

    private readonly IScriptGeneratorService _scriptGenerator;

    public HighlighterService(InteropService interopService, IAppConfiguration appConfiguration, IScriptGeneratorService scriptGenerator)
    {
        _interopService = interopService;
        _appConfiguration = appConfiguration;
        _scriptGenerator = scriptGenerator;
    }

    public MarkupString GetDisplayableScript(Workspace workspace, bool startChevron = true)
    {
        var formattedScript = GetFormattedScript(_scriptGenerator.GetScript(workspace), startChevron);
        return new MarkupString(string.Format(template, string.Empty, formattedScript));
    }

    public async Task<MarkupString> GetHiglightedScript(Workspace workspace, bool startChevron = true)
    {
        var highlightedScript = await _interopService.GetHighlightedPowerShellScript(_scriptGenerator.GetScript(workspace));
        var cssClass = GetHighlightClassByTheme(_appConfiguration.GetTheme());
        var formattedScript = GetFormattedScript(highlightedScript, startChevron);

        return new MarkupString(string.Format(template, cssClass, formattedScript));
    }

    private static string GetFormattedScript(string script, bool startChevron)
    {
        var builder = new StringBuilder();

        foreach (var line in script.Split('\n'))
        {
            if (startChevron) builder.Append("> ");
            builder.Append(line).Append("<br />");
        }

        return builder.ToString();
    }

    private static string GetHighlightClassByTheme(Theme theme)
    {
        return theme switch
        {
            Theme.Light => LightClass,
            Theme.Dark => DarkClass,
            _ => throw new NotImplementedException($"{nameof(Theme)}.{theme} wasn't recognize as a valid theme highlightable class!"),
        };
    }
}
