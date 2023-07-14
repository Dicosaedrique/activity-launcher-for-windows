using FastWorkspace.Domain.Exceptions;
using FastWorkspace.Domain.Extensions;
using FastWorkspace.Domain.Utils;

namespace FastWorkspace.Domain.Services;

public class WorkspaceStore : IWorkspaceStore
{
    private const string WorkspaceDirectory = "Workspaces";

    private readonly IFileStorage _fileStorage;

    private List<Workspace>? _workspaces;

    public WorkspaceStore(IFileStorage fileStorage)
    {
        _fileStorage = fileStorage;
    }

    public async Task<ResultWithContent<IEnumerable<Workspace>>> GetAllAsync(bool forceRefresh = false)
    {
        if (_workspaces == null || forceRefresh)
        {
            var result = await ReadWorkspaces();

            if (result.Failure)
            {
                return new ResultWithContent<IEnumerable<Workspace>>(result.Exception!);
            }
        }

        return new ResultWithContent<IEnumerable<Workspace>>(_workspaces!.AsEnumerable());
    }

    public async Task<ResultWithContent<Workspace>> GetByIdAsync(Guid id, bool forceRefresh = false)
    {
        if (_workspaces == null || forceRefresh)
        {
            var result = await ReadWorkspaces();

            if (result.Failure)
            {
                return new ResultWithContent<Workspace>(result.Exception!);
            }
        }

        var workspace = _workspaces?.FirstOrDefault(x => x.Id == id);

        if (workspace == null)
        {
            return new ResultWithContent<Workspace>(new NotFoundException($"The workspace with id \"{id}\" was not found"));
        }
        else
        {
            return new ResultWithContent<Workspace>(workspace);
        }
    }

    public Task<Result> AddOrUpdateAsync(Workspace workspace)
    {
        _workspaces = null;
        return _fileStorage.WriteFileAsync(GetWorkspaceFilePath(workspace), workspace);
    }

    public Task<Result> DeleteAsync(Workspace workspace)
    {
        _workspaces = null;
        return Task.FromResult(_fileStorage.DeleteFile(GetWorkspaceFilePath(workspace)));
    }

    private async Task<Result> ReadWorkspaces()
    {
        _workspaces = null;

        var workspaces = new List<Workspace>();

        foreach (var workspaceFilePath in GetWorkspaceFilePaths())
        {
            var result = await _fileStorage.ReadFileAsync<Workspace>(workspaceFilePath);

            if (result.Success)
            {
                workspaces.Add(result.Content!);
            }
            else
            {
                return result;
            }
        }

        _workspaces = workspaces;

        return Result.SuccessResult;
    }

    private string GetWorkspacePath()
    {
        return _fileStorage.GetStorageFilePath(WorkspaceDirectory);
    }

    private IEnumerable<string> GetWorkspaceFilePaths()
    {
        return _fileStorage.GetFiles(GetWorkspacePath(), "*.json");
    }

    private string GetWorkspaceFilePath(Workspace workspace)
    {
        return Path.Combine(GetWorkspacePath(), workspace.GetFileName());
    }
}
