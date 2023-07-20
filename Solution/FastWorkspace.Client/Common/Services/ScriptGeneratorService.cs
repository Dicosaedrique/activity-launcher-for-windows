using FastWorkspace.Domain.Interfaces;
using FastWorkspace.Domain.Model;
using FastWorkspace.Domain.Services.Declarations;

namespace FastWorkspace.Client.Common.Services;

public class ScriptGeneratorService : IScriptGeneratorService
{
    public string GetScript(Workspace workspace)
    {
        return string.Empty; // todo
    }

    public string GetScript(IJob job)
    {
        return string.Empty; // todo
    }

    /*public string GetScript()
    {
        var builder = new StringBuilder();
        builder.Append(GetScriptHeader());

        var readyJobs = _jobs.Where(x => x.Enabled).OrderBy(x => x.Sequence);

        foreach (var job in readyJobs)
        {
            builder.Append(GetJobScript(job));
        }

        builder.Append(GetScriptFooter());

        return builder.ToString();
    }

    private string GetScriptHeader()
    {
        const string template = "##################################################################################################\n# Workspace: {0}\n#\n# Script generated with love by Fast Workspace <3\n##################################################################################################\n\n#region JOBS\n\n";
        const string templateWithDescription = "##################################################################################################\n# Workspace: {0}\n# Description: {1}\n#\n# Script generated with love by Fast Workspace <3\n##################################################################################################\n\n#region JOBS\n\n";

        return string.IsNullOrWhiteSpace(Description)
        ? string.Format(template, Name)
            : string.Format(templateWithDescription, Name, Description);
    }

    private string GetJobScript(IJob job)
    {
        const string template = "###################################\n# Job {0} - {1}\n\n{2}\n\n\n";
        const string templateWithDescription = "###################################\n# Job {0} - {1}\n# Description: {2}\n\n{3}\n\n\n";

        return string.IsNullOrWhiteSpace(job.Description)
            ? string.Format(template, job.Sequence + 1, job.DisplayName, job.GetScript())
            : string.Format(templateWithDescription, job.Sequence + 1, job.DisplayName, job.Description, job.GetScript());
    }

    private string GetScriptFooter()
    {
        const string template = "#endregion JOBS\n\n# Script generated the {0:d} at {0:t}\n# Thanks for using Fast Workspace!";
        return string.Format(template, DateTime.Now);
    }*/
}
