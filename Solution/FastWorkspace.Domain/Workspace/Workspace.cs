using FastWorkspace.Domain.Interfaces;
using System.Text;

namespace FastWorkspace.Domain.Workspace;

public class Workspace : IScriptable
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool Enabled { get; set; } = true;

    public DateTime CreationDate { get; init; } = DateTime.Now;

    public DateTime LastModifiedDate { get; set; }

    private readonly List<IJob> _jobs = new();

    public void AddJob(IJob job)
    {
        _jobs.Add(job);
    }

    public bool RemoveJob(IJob job)
    {
        return _jobs.Remove(job);
    }

    public void ClearJobs()
    {
        _jobs.Clear();
    }

    public string GetScript()
    {
        var builder = new StringBuilder();

        builder.Append(GetScriptHeader());

        foreach (var job in _jobs)
        {
            builder.Append(GetJobScript(job));
        }

        builder.Append(GetScriptFooter());

        return builder.ToString();
    }

    private string GetScriptHeader()
    {
        const string template = "##################################################################################################\n# Workspace: {0}\n# Description: {1}\n#\n# Script generated with love by Fast Workspace <3\n##################################################################################################\n\n#region JOBS\n\n";
        return string.Format(template, Name, Description);
    }

    private string GetJobScript(IJob job)
    {
        const string template = "###################################\n# Job {0} - {1} ({2})\n\n{5}\n\n\n";
        return string.Format(template, job.Sequence, job.Name, job.Description, job.GetScript());
    }

    private string GetScriptFooter()
    {
        const string template = "#endregion JOBS\n\n# Script generated the {0:d} at {0:t}\n\n# Thanks for using Fast Workspace!";
        return string.Format(template, DateTime.Now);
    }
}
