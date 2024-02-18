using ActivityLauncher.Client.Locales;
using ActivityLauncher.Domain.Interfaces;
using ActivityLauncher.Domain.Model;
using ActivityLauncher.Domain.Services.Declarations;
using Microsoft.Extensions.Localization;
using System.Text;

namespace ActivityLauncher.Client.Core.Services;

public class ScriptGeneratorService : IScriptGeneratorService
{
    private readonly IStringLocalizer<ScriptLocales> _localize;

    public ScriptGeneratorService(IStringLocalizer<ScriptLocales> localization)
    {
        _localize = localization;
    }

    public string GetScript(Activity activity)
    {
        var builder = new StringBuilder();

        builder.Append(GetScriptHeader(activity));

        foreach (var task in activity.GetTasks())
        {
            builder.Append(GetTaskScript(task));
        }

        builder.Append(GetScriptFooter());

        return builder.ToString();
    }

    private string GetScriptHeader(Activity activity)
    {
        if (string.IsNullOrWhiteSpace(activity.Description))
        {
            return string.Format(HeaderWithoutDescriptionTemplate,
                _localize["Generator.Header.Name"],
                activity.Name,
                _localize["Generator.Header.Message"]);
        }

        return string.Format(HeaderTemplate,
            _localize["Generator.Header.Name"],
            activity.Name,
            _localize["Generator.Header.Description"],
            activity.Description,
            _localize["Generator.Header.Message"]);
    }

    private string GetTaskScript(ITask task)
    {
        var builder = new StringBuilder();

        if (!string.IsNullOrWhiteSpace(task.Name))
        {
            builder.AppendLine($"# {task.Name}");
        }

        builder.Append(task.GetScript());

        return string.Format(TaskTemplate, builder.ToString());
    }

    private string GetScriptFooter()
    {
        return string.Format(FooterTemplate,
            string.Format(_localize["Generator.Footer.Timestamp"], DateTime.Now),
            _localize["Generator.Footer.Message"]);
    }

    #region Templates

    private const string HeaderTemplate = "###################################################################################################\n# {0}: {1}\n# {2}: {3}\n#\n# {4}\n###################################################################################################\n\n";

    private const string HeaderWithoutDescriptionTemplate = "###################################################################################################\n# {0}: {1}\n#\n# {2}\n###################################################################################################\n\n";

    private const string TaskTemplate = "{0}\n\n";

    private const string FooterTemplate = "# {0}\n# {1}\n###################################################################################################";

    #endregion
}
