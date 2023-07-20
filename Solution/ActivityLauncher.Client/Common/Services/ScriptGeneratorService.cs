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

    public string GetScript(Workspace workspace)
    {
        var builder = new StringBuilder();

        builder.Append(GetScriptHeader(workspace));

        builder.Append(GetScriptJobSection(_localize["Generator.Section.BrowserTabsJob"], workspace.BrowserTabsJobs));
        builder.Append(GetScriptJobSection(_localize["Generator.Section.ExplorerFolderJob"], workspace.ExplorerFolderJobs));
        builder.Append(GetScriptJobSection(_localize["Generator.Section.ProgramJob"], workspace.ProgramJobs));
        builder.Append(GetScriptJobSection(_localize["Generator.Section.ScriptJob"], workspace.ScriptJobs));
        builder.Append(GetScriptJobSection(_localize["Generator.Section.TerminalJob"], workspace.TerminalJobs));
        builder.Append(GetScriptJobSection(_localize["Generator.Section.VSCodeJob"], workspace.VSCodeJobs));
        builder.Append(GetScriptJobSection(_localize["Generator.Section.VSSolutionJob"], workspace.VSSolutionJobs));

        builder.Append(GetScriptFooter());

        return builder.ToString();
    }

    private string GetScriptHeader(Workspace workspace)
    {
        if (string.IsNullOrWhiteSpace(workspace.Description))
        {
            return string.Format(HeaderWithoutDescriptionTemplate,
                _localize["Generator.Header.Name"],
                workspace.Name,
                _localize["Generator.Header.Message"]);
        }

        return string.Format(HeaderTemplate,
            _localize["Generator.Header.Name"],
            workspace.Name,
            _localize["Generator.Header.Description"],
            workspace.Description,
            _localize["Generator.Header.Message"]);
    }

    private string GetScriptJobSection(string title, IEnumerable<IJob> jobs)
    {
        var enabledJobs = jobs.Where(job => job.Enabled);

        if(enabledJobs.Any())
        {
            var jobsScript = string.Join("\n\n", enabledJobs.Select(GetJobScript));
            return string.Format(JobSectionTemplate, title, jobsScript);
        }

        return string.Empty;
    }

    private string GetJobScript(IJob job)
    {
        if (!job.Enabled) return string.Empty;

        var builder = new StringBuilder();

        if (!string.IsNullOrWhiteSpace(job.Name))
        {
            builder.AppendLine($"# {job.Name}");
        }

        builder.Append(job.GetScript());

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

    private const string JobSectionTemplate = "######################################################\n#region [{0}]\n\n{1}\n\n#endregion ###########################################\n\n\n\n";

    private const string FooterTemplate = "# {0}\n# {1}\n###################################################################################################";

    #endregion
}
