using ActivityLauncher.Domain.Interfaces;
using ActivityLauncher.Domain.Model;

namespace ActivityLauncher.Domain.Common.Extensions;

public static class ActivityExtensions
{
    public static IEnumerable<ITask> GetTasks(this Activity activity)
    {
        foreach (var task in activity.BrowserTabsTasks) yield return task;
        foreach (var task in activity.ExplorerFolderTasks) yield return task;
        foreach (var task in activity.ProgramTasks) yield return task;
        foreach (var task in activity.ScriptTasks) yield return task;
        foreach (var task in activity.TerminalTasks) yield return task;
        foreach (var task in activity.VSCodeTasks) yield return task;
        foreach (var task in activity.VSSolutionTasks) yield return task;
    }

    public static IEnumerable<IEnumerable<ITask>> GetTasksList(this Activity activity)
    {
        yield return activity.BrowserTabsTasks;
        yield return activity.ExplorerFolderTasks;
        yield return activity.ProgramTasks;
        yield return activity.ScriptTasks;
        yield return activity.TerminalTasks;
        yield return activity.VSCodeTasks;
        yield return activity.VSSolutionTasks;
    }
}
