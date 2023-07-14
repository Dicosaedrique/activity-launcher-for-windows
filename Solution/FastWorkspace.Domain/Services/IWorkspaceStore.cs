using FastWorkspace.Domain.Utils;

namespace FastWorkspace.Domain.Services;

public interface IWorkspaceStore
{
    public Task<ResultWithContent<IEnumerable<Workspace>>> GetAllAsync(bool forceRefresh = false);

    public Task<ResultWithContent<Workspace>> GetByIdAsync(Guid id, bool forceRefresh = false);

    public Task<Result> AddOrUpdateAsync(Workspace workspace);

    public Task<Result> DeleteAsync(Workspace workspace);
}
