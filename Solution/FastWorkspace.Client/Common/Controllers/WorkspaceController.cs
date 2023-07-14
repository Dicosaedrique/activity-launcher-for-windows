using FastWorkspace.Domain.Services;

namespace FastWorkspace.Client.Common.Controllers;
public class WorkspaceController
{
    private readonly IWorkspaceStore _workspaceStore;

    public WorkspaceController(IWorkspaceStore workspaceStore)
    {
        _workspaceStore = workspaceStore;
    }
}
