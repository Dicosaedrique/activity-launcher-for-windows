using ActivityLauncher.Domain.Enums;

namespace ActivityLauncher.Client.Common.Utils;

public static class TaskTypeExtensions
{
    public static string GetIconSource(this TaskType taskType)
    {
        return taskType switch
        {
            TaskType.ExplorerFolderTask => "/img/ExplorerFolderTaskIcon.png",
            TaskType.ProgramTask => "/img/ProgramTaskIcon.png",
            TaskType.ScriptTask => "/img/ScriptTaskIcon.png",
            TaskType.TerminalTask => "/img/TerminalTaskIcon.png",
            TaskType.VSCodeTask => "/img/VSCodeTaskIcon.png",
            TaskType.OpenFileTask => "/img/OpenFileTaskIcon.png",
            _ => throw new NotImplementedException(),
        };
    }
}
