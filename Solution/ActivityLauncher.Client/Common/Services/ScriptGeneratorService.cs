using System.Text;
using ActivityLauncher.Client.Locales;
using ActivityLauncher.Domain.Interfaces;
using ActivityLauncher.Domain.Model;
using ActivityLauncher.Domain.Services.Declarations;
using Microsoft.Extensions.Localization;

namespace ActivityLauncher.Client.Common.Services;

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

        builder.Append(GetScriptTaskSection(_localize["Generator.Section.BrowserTabsTask"], activity.BrowserTabsTasks));
        builder.Append(GetScriptTaskSection(_localize["Generator.Section.ExplorerFolderTask"], activity.ExplorerFolderTasks));
        builder.Append(GetScriptTaskSection(_localize["Generator.Section.ProgramTask"], activity.ProgramTasks));
        builder.Append(GetScriptTaskSection(_localize["Generator.Section.ScriptTask"], activity.ScriptTasks));
        builder.Append(GetScriptTaskSection(_localize["Generator.Section.TerminalTask"], activity.TerminalTasks));
        builder.Append(GetScriptTaskSection(_localize["Generator.Section.VSCodeTask"], activity.VSCodeTasks));
        builder.Append(GetScriptTaskSection(_localize["Generator.Section.VSSolutionTask"], activity.VSSolutionTasks));

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

    private string GetScriptTaskSection(string title, IEnumerable<ITask> tasks)
    {
        var enabledTasks = tasks.Where(task => task.Enabled);

        if(enabledTasks.Any())
        {
            var tasksScript = string.Join("\n\n", enabledTasks.Select(GetTaskScript));
            return string.Format(TaskSectionTemplate, _localize["Generator.Section.Prefix"], title, tasksScript);
        }

        return string.Empty;
    }

    private string GetTaskScript(ITask task)
    {
        if (!task.Enabled) return string.Empty;

        var builder = new StringBuilder();

        if (!string.IsNullOrWhiteSpace(task.Name))
        {
            builder.AppendLine($"# {task.Name}");
        }

        builder.Append(task.GetScript());

        return builder.ToString();
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

    private const string TaskSectionTemplate = "######################################################\n#region [{0}] {1}\n\n{2}\n\n#endregion ###########################################\n\n\n\n";

    private const string FooterTemplate = "# {0}\n# {1}\n###################################################################################################";

    #endregion
}
