using FastWorkspace.Domain.Utils;

namespace FastWorkspace.Domain.Services;

public interface IWorkspaceStore
{
    public Task<ResultWithContent<IEnumerable<Workspace>>> GetAllAsync();

    public Task<ResultWithContent<Workspace>> GetByIdAsync(Guid id);

    public Task<Result> AddOrUpdateAsync(Workspace workspace);

    public Task<Result> DeleteAsync(Workspace workspace);

    public void SetupStore();
}
