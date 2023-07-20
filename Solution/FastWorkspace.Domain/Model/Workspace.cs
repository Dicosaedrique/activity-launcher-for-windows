using FastWorkspace.Domain.Interfaces;
using FastWorkspace.Domain.Model.Jobs;

namespace FastWorkspace.Domain.Model;

public class Workspace : ICloneable<Workspace>
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime CreationDate { get; set; } = DateTime.Now;

    public DateTime LastModifiedDate { get; set; } = DateTime.Now;

    public List<BrowserTabsJob> BrowserTabsJobs { get; init; } = new();

    public List<ExplorerFolderJob> ExplorerFolderJobs { get; init; } = new();

    public List<ProgramFileJob> ProgramFileJobs { get; init; } = new();

    public List<ScriptJob> ScriptJobs { get; init; } = new();

    public List<TerminalJob> TerminalJobs { get; init; } = new();

    public List<TerminalTabsJob> TerminalTabsJobs { get; init; } = new();

    public List<VSCodeJob> VSCodeJobs { get; init; } = new();

    public List<VSSolutionJob> VSSolutionJobs { get; init; } = new();

    public Workspace Clone()
    {
        return new Workspace()
        {
            Id = Id,
            Name = Name,
            Description = Description,
            CreationDate = CreationDate,
            LastModifiedDate = LastModifiedDate,
            BrowserTabsJobs = BrowserTabsJobs.Select(x => x.Clone()).ToList(),
            ExplorerFolderJobs = ExplorerFolderJobs.Select(x => x.Clone()).ToList(),
            ProgramFileJobs = ProgramFileJobs.Select(x => x.Clone()).ToList(),
            ScriptJobs = ScriptJobs.Select(x => x.Clone()).ToList(),
            TerminalJobs = TerminalJobs.Select(x => x.Clone()).ToList(),
            TerminalTabsJobs = TerminalTabsJobs.Select(x => x.Clone()).ToList(),
            VSCodeJobs = VSCodeJobs.Select(x => x.Clone()).ToList(),
            VSSolutionJobs = VSSolutionJobs.Select(x => x.Clone()).ToList(),
        };
    }
}
