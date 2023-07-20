using FastWorkspace.Domain.Common.Extensions;
using FastWorkspace.Domain.Common.Utils;
using FastWorkspace.Domain.Model;
using FastWorkspace.Domain.Services.Declarations;

namespace FastWorkspace.Domain.Services;

public class WorkspaceStore : IWorkspaceStore
{
    private const string WorkspaceDirectory = "Workspaces";

    private readonly IFileStorage _fileStorage;

    public WorkspaceStore(IFileStorage fileStorage)
    {
        _fileStorage = fileStorage;
    }

    public async Task<ResultWithContent<IEnumerable<Workspace>>> GetAllAsync()
    {
        var errors = new List<Exception>();
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
                errors.Add(result.Exception!);
            }
        }

        if (errors.Any())
        {
            return new ResultWithContent<IEnumerable<Workspace>>(new AggregateException(errors));
        }

        return new ResultWithContent<IEnumerable<Workspace>>(workspaces);

    }

    public Task<ResultWithContent<Workspace>> GetByIdAsync(Guid id)
    {
        return _fileStorage.ReadFileAsync<Workspace>(GetWorkspaceFilePathById(id));
    }

    public Task<Result> AddOrUpdateAsync(Workspace workspace)
    {
        return _fileStorage.WriteFileAsync(GetWorkspaceFilePath(workspace), workspace);
    }

    public Task<Result> DeleteAsync(Workspace workspace)
    {
        return Task.FromResult(_fileStorage.DeleteFile(GetWorkspaceFilePath(workspace)));
    }

    public string GetWorkspaceStorageDirectoryPath()
    {
        return _fileStorage.GetStorageFilePath(WorkspaceDirectory);
    }

    private IEnumerable<string> GetWorkspaceFilePaths()
    {
        return _fileStorage.GetFiles(GetWorkspaceStorageDirectoryPath(), "*.json");
    }

    private string GetWorkspaceFilePath(Workspace workspace)
    {
        return Path.Combine(GetWorkspaceStorageDirectoryPath(), workspace.GetFileName());
    }

    private string GetWorkspaceFilePathById(Guid id)
    {
        return Path.Combine(GetWorkspaceStorageDirectoryPath(), WorkspaceExtensions.GetWorkspaceFileNameById(id));
    }

    public void SetupStore()
    {
        _fileStorage.CreateDirectoryIfNotExists(WorkspaceDirectory);
    }
}
