namespace FastWorkspace.Domain.Extensions;

public static class WorkspaceExtensions
{
    public static string GetFileName(this Workspace workspace)
    {
        return $"{workspace.Id}.json";
    }
}
