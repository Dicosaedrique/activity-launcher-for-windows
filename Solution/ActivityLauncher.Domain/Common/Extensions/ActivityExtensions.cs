using ActivityLauncher.Domain.Interfaces;
using ActivityLauncher.Domain.Model;
using ActivityLauncher.Domain.Model.Tasks;

namespace ActivityLauncher.Domain.Common.Extensions;

public static class ActivityExtensions
{
    public static void AddTask(this Activity activity, ITask task)
    {
        switch (task)
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
