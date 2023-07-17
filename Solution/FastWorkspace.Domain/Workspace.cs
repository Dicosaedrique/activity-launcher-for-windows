using System.Text;
using FastWorkspace.Domain.Converters;
using FastWorkspace.Domain.Interfaces;
using Newtonsoft.Json;

namespace FastWorkspace.Domain;

[JsonConverter(typeof(WorkspaceConverter))]
public class Workspace : IScriptable, ICloneable<Workspace>
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime CreationDate { get; set; } = DateTime.Now;

    public DateTime LastModifiedDate { get; set; } = DateTime.Now;

    public IEnumerable<IJob> Jobs => _jobs.AsEnumerable();

    public IEnumerable<IJob> SortedJobs => _jobs.OrderBy(x => x.Sequence);

    private readonly List<IJob> _jobs = new();

    public Workspace() { }

    public Workspace(Guid id, string name, string? description, DateTime creationDate, DateTime lastModifiedDate, IEnumerable<IJob> jobs)
    {
        Id = id;
        Name = name;
        Description = description;
        CreationDate = creationDate;
        LastModifiedDate = lastModifiedDate;
        _jobs = jobs.OrderBy(x => x.Sequence).ToList();
        UpdateJobSequences();
    }

    public void AddJob(IJob job)
    {
        _jobs.Add(job);
        UpdateJobSequences();
    }

    public bool RemoveJob(IJob job)
    {
        var removed = _jobs.Remove(job);
        UpdateJobSequences();
        return removed;
    }

    public void ClearJobs()
    {
        _jobs.Clear();
    }

    public string GetScript()
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
    }

    private void UpdateJobSequences()
    {
        for (var i = 0; i < _jobs.Count; i++)
        {
            _jobs[i].Sequence = i;
        }
    }

    public Workspace Clone()
    {
        return new Workspace(Id, Name, Description, CreationDate, LastModifiedDate, _jobs.Select(x => x.Clone()));
    }
}
