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

    public List<ExplorerFolderTask> ExplorerFolderTasks { get; init; } = new();

    public List<ProgramTask> ProgramTasks { get; init; } = new();

    public List<ScriptTask> ScriptTasks { get; init; } = new();

    public List<TerminalTask> TerminalTasks { get; init; } = new();

    public List<VSCodeTask> VSCodeTasks { get; init; } = new();

    public List<OpenFileTask> OpenFileTasks { get; init; } = new();

    public void AddTask(ExplorerFolderTask task) => ExplorerFolderTasks.Add(task);
    public void AddTask(ProgramTask task) => ProgramTasks.Add(task);
    public void AddTask(ScriptTask task) => ScriptTasks.Add(task);
    public void AddTask(TerminalTask task) => TerminalTasks.Add(task);
    public void AddTask(VSCodeTask task) => VSCodeTasks.Add(task);
    public void AddTask(OpenFileTask task) => OpenFileTasks.Add(task);

    public bool RemoveTask(ExplorerFolderTask task) => ExplorerFolderTasks.Remove(task);
    public bool RemoveTask(ProgramTask task) => ProgramTasks.Remove(task);
    public bool RemoveTask(ScriptTask task) => ScriptTasks.Remove(task);
    public bool RemoveTask(TerminalTask task) => TerminalTasks.Remove(task);
    public bool RemoveTask(VSCodeTask task) => VSCodeTasks.Remove(task);
    public bool RemoveTask(OpenFileTask task) => OpenFileTasks.Remove(task);

    public IEnumerable<ITask> GetTasks()
    {
        var tasks = new List<ITask>();

        tasks.AddRange(ExplorerFolderTasks);
        tasks.AddRange(ProgramTasks);
        tasks.AddRange(ScriptTasks);
        tasks.AddRange(TerminalTasks);
        tasks.AddRange(VSCodeTasks);
        tasks.AddRange(OpenFileTasks);

        return tasks.OrderBy(x => x.CreationDate);
    }

    public Activity Clone()
    {
        return new Activity()
        {
            Id = Id,
            Name = Name,
            Description = Description,
            CreationDate = CreationDate,
            LastModifiedDate = LastModifiedDate,
            ExplorerFolderTasks = ExplorerFolderTasks.Select(x => x.Clone()).ToList(),
            ProgramTasks = ProgramTasks.Select(x => x.Clone()).ToList(),
            ScriptTasks = ScriptTasks.Select(x => x.Clone()).ToList(),
            TerminalTasks = TerminalTasks.Select(x => x.Clone()).ToList(),
            VSCodeTasks = VSCodeTasks.Select(x => x.Clone()).ToList(),
            OpenFileTasks = OpenFileTasks.Select(x => x.Clone()).ToList(),
        };
    }

    // todo: temp demo
    public static void InitializeDemoTasks(Activity activity)
    {
        activity.ExplorerFolderTasks.Add(new ExplorerFolderTask() { DirectoryPath = "C:\\Dev" });
        activity.ExplorerFolderTasks.Add(new ExplorerFolderTask() { DirectoryPath = "C:\\Dev\\Perso" });
        activity.ExplorerFolderTasks.Add(new ExplorerFolderTask() { DirectoryPath = "C:\\Dev\\Perso" });
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
        activity.OpenFileTasks.Add(new OpenFileTask() { FilePath = "C:\\Dev\\Perso\\activity-launcher-for-windows\\Solution\\ActivityLauncher.sln" });
    }
}
