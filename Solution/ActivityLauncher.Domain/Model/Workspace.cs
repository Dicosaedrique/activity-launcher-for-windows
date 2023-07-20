using ActivityLauncher.Domain.Interfaces;
using ActivityLauncher.Domain.Model.Jobs;
using ActivityLauncher.Domain.Model.Terminal;

namespace ActivityLauncher.Domain.Model;

public class Workspace : ICloneable<Workspace>
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime CreationDate { get; set; } = DateTime.Now;

    public DateTime LastModifiedDate { get; set; } = DateTime.Now;

    public List<BrowserTabsJob> BrowserTabsJobs { get; init; } = new();

    public List<ExplorerFolderJob> ExplorerFolderJobs { get; init; } = new();

    public List<ProgramJob> ProgramJobs { get; init; } = new();

    public List<ScriptJob> ScriptJobs { get; init; } = new();

    public List<TerminalJob> TerminalJobs { get; init; } = new();

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
            ProgramJobs = ProgramJobs.Select(x => x.Clone()).ToList(),
            ScriptJobs = ScriptJobs.Select(x => x.Clone()).ToList(),
            TerminalJobs = TerminalJobs.Select(x => x.Clone()).ToList(),
            VSCodeJobs = VSCodeJobs.Select(x => x.Clone()).ToList(),
            VSSolutionJobs = VSSolutionJobs.Select(x => x.Clone()).ToList(),
        };
    }

    // todo: temp demo
    public static void InitializeDemoJobs(Workspace workspace)
    {
        workspace.BrowserTabsJobs.Add(new BrowserTabsJob() { Name = "Open my favorite tabs" });
        workspace.ExplorerFolderJobs.Add(new ExplorerFolderJob() { DirectoryPath = "C:\\Dev" });
        workspace.ExplorerFolderJobs.Add(new ExplorerFolderJob() { DirectoryPath = "C:\\Dev\\Perso" });
        workspace.ExplorerFolderJobs.Add(new ExplorerFolderJob() { DirectoryPath = "C:\\Dev\\Perso", Enabled = false });
        workspace.ProgramJobs.Add(new ProgramJob() { ProgramName = "firefox" });
        workspace.ScriptJobs.Add(new ScriptJob() { Name = "My script to restore peace on earth", Script = "# Some big fancy script" });
        workspace.TerminalJobs.Add(new TerminalJob()
        {
            Name = "Open lots of tabs",
            TerminalTabs = new()
            {
                new TerminalTab() { Title = "Hello world", Color = "#166af2" },
                new TerminalTab() { Color = "#14ba0b", LocationPath = "C:\\Dev", Command = "ls" }
            }
        });
        workspace.TerminalJobs.Add(new TerminalJob() { Name = "Open lots of tabs", TerminalTabs = new() { new() { LocationPath = "C:\\Dev\\Perso" } } });
        workspace.VSCodeJobs.Add(new VSCodeJob() { DirectoryPath = "C:\\Dev\\Perso" });
        workspace.VSSolutionJobs.Add(new VSSolutionJob() { SolutionFilePath = "C:\\Dev\\Perso\\fast-workspace\\Solution\\ActivityLauncher.sln" });
    }
}
