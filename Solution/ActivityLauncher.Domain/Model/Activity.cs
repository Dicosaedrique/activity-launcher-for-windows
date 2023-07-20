using ActivityLauncher.Domain.Interfaces;
using ActivityLauncher.Domain.Model.Tasks;
using ActivityLauncher.Domain.Model.Terminal;

namespace ActivityLauncher.Domain.Model;

public class Activity : ICloneable<Activity>
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime CreationDate { get; set; } = DateTime.Now;

    public DateTime LastModifiedDate { get; set; } = DateTime.Now;

    public List<BrowserTabsTask> BrowserTabsTasks { get; init; } = new();

    public List<ExplorerFolderTask> ExplorerFolderTasks { get; init; } = new();

    public List<ProgramTask> ProgramTasks { get; init; } = new();

    public List<ScriptTask> ScriptTasks { get; init; } = new();

    public List<TerminalTask> TerminalTasks { get; init; } = new();

    public List<VSCodeTask> VSCodeTasks { get; init; } = new();

    public List<VSSolutionTask> VSSolutionTasks { get; init; } = new();

    public Activity Clone()
    {
        return new Activity()
        {
            Id = Id,
            Name = Name,
            Description = Description,
            CreationDate = CreationDate,
            LastModifiedDate = LastModifiedDate,
            BrowserTabsTasks = BrowserTabsTasks.Select(x => x.Clone()).ToList(),
            ExplorerFolderTasks = ExplorerFolderTasks.Select(x => x.Clone()).ToList(),
            ProgramTasks = ProgramTasks.Select(x => x.Clone()).ToList(),
            ScriptTasks = ScriptTasks.Select(x => x.Clone()).ToList(),
            TerminalTasks = TerminalTasks.Select(x => x.Clone()).ToList(),
            VSCodeTasks = VSCodeTasks.Select(x => x.Clone()).ToList(),
            VSSolutionTasks = VSSolutionTasks.Select(x => x.Clone()).ToList(),
        };
    }

    // todo: temp demo
    public static void InitializeDemoTasks(Activity activity)
    {
        activity.BrowserTabsTasks.Add(new BrowserTabsTask() { Name = "Open my favorite tabs" });
        activity.ExplorerFolderTasks.Add(new ExplorerFolderTask() { DirectoryPath = "C:\\Dev" });
        activity.ExplorerFolderTasks.Add(new ExplorerFolderTask() { DirectoryPath = "C:\\Dev\\Perso" });
        activity.ExplorerFolderTasks.Add(new ExplorerFolderTask() { DirectoryPath = "C:\\Dev\\Perso", Enabled = false });
        activity.ProgramTasks.Add(new ProgramTask() { ProgramName = "firefox" });
        activity.ScriptTasks.Add(new ScriptTask() { Name = "My script to restore peace on earth", Script = "# Some big fancy script" });
        activity.TerminalTasks.Add(new TerminalTask()
        {
            Name = "Open lots of tabs",
            TerminalTabs = new()
            {
                new TerminalTab() { Title = "Hello world", Color = "#166af2" },
                new TerminalTab() { Color = "#14ba0b", LocationPath = "C:\\Dev", Command = "ls" }
            }
        });
        activity.TerminalTasks.Add(new TerminalTask() { TerminalTabs = new() { new() { LocationPath = "C:\\Dev\\Perso" } } });
        activity.VSCodeTasks.Add(new VSCodeTask() { DirectoryPath = "C:\\Dev\\Perso" });
        activity.VSSolutionTasks.Add(new VSSolutionTask() { SolutionFilePath = "C:\\Dev\\Perso\\activity-launcher-for-windows\\Solution\\ActivityLauncher.sln" });
    }
}
