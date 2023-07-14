namespace FastWorkspace.Domain.Extensions;

public static class WorkspaceExtensions
{
    public static string GetFileName(this Workspace workspace) => GetWorkspaceFileNameById(workspace.Id);

    public static string GetWorkspaceFileNameById(Guid id) => $"{id}.json";
}
