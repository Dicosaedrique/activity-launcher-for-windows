using ActivityLauncher.Domain.Interfaces;
using ActivityLauncher.Domain.Model;
using ActivityLauncher.Domain.Model.Tasks;

namespace ActivityLauncher.Domain.Common.Extensions;

public static class ActivityExtensions
{
    public static IEnumerable<ITask> GetTasks(this Activity activity)
    {
        foreach (var task in activity.ExplorerFolderTasks) yield return task;
        foreach (var task in activity.ProgramTasks) yield return task;
        foreach (var task in activity.ScriptTasks) yield return task;
        foreach (var task in activity.TerminalTasks) yield return task;
        foreach (var task in activity.VSCodeTasks) yield return task;
        foreach (var task in activity.OpenFileTasks) yield return task;
    }

    public static IEnumerable<IEnumerable<ITask>> GetTasksList(this Activity activity)
    {
        yield return activity.ExplorerFolderTasks;
        yield return activity.ProgramTasks;
        yield return activity.ScriptTasks;
        yield return activity.TerminalTasks;
        yield return activity.VSCodeTasks;
        yield return activity.OpenFileTasks;
    }

    public static void AddTask(this Activity activity, ITask task)
    {
        switch(task)
        {
            case ExplorerFolderTask explorerFolderTask:
                activity.AddTask(explorerFolderTask);
                break;
            case ProgramTask programTask:
                activity.AddTask(programTask);
                break;
            case ScriptTask scriptTask:
                activity.AddTask(scriptTask);
                break;
            case TerminalTask terminalTask:
                activity.AddTask(terminalTask);
                break;
            case VSCodeTask vSCodeTask:
                activity.AddTask(vSCodeTask);
                break;
            case OpenFileTask openFileTask:
                activity.AddTask(openFileTask);
                break;
            default:
                throw new NotImplementedException($"{task.GetType().Name} is not managed by the generic Activity.AddTask extension");
        }
    }

    public static bool RemoveTask(this Activity activity, ITask task)
    {
        return task switch
        {
            ExplorerFolderTask explorerFolderTask => activity.RemoveTask(explorerFolderTask),
            ProgramTask programTask => activity.RemoveTask(programTask),
            ScriptTask scriptTask => activity.RemoveTask(scriptTask),
            TerminalTask terminalTask => activity.RemoveTask(terminalTask),
            VSCodeTask vSCodeTask => activity.RemoveTask(vSCodeTask),
            OpenFileTask openFileTask => activity.RemoveTask(openFileTask),
            _ => throw new NotImplementedException($"{task.GetType().Name} is not managed by the generic Activity.RemoveTask extension"),
        };
    }
}
