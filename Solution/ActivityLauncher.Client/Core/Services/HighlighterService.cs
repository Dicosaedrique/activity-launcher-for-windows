using ActivityLauncher.Domain.Enums;
using ActivityLauncher.Domain.Model;
using ActivityLauncher.Domain.Services.Declarations;
using Microsoft.AspNetCore.Components;
using System.Text;

namespace ActivityLauncher.Client.Core.Services;

public class HighlighterService
{
    private const string Template = "<code style=\"font-size: 1rem;\" class=\"{0}\">{1}</code>";

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

    public Task<MarkupString> GetDisplayableScriptAsync(Activity activity, bool highlight, bool startChevron)
        => GetDisplayableScriptAsync(_scriptGenerator.GetScript(activity), highlight, startChevron);

    public async Task<MarkupString> GetDisplayableScriptAsync(string script, bool highlight, bool startChevron)
    {
        if (highlight)
        {
            var highlightedScript = await _interopService.GetHighlightedPowerShellScript(script);
            var cssClass = GetHighlightClassByTheme(_appConfiguration.GetTheme());
            var formattedScript = GetFormattedScript(highlightedScript, startChevron);

            return new MarkupString(string.Format(Template, cssClass, formattedScript));
        }
        else
        {
            var formattedScript = GetFormattedScript(script, startChevron);
            return new MarkupString(string.Format(Template, string.Empty, formattedScript));
        }
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
