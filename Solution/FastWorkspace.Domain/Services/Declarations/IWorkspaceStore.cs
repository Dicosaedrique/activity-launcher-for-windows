using FastWorkspace.Domain.Common.Utils;
using FastWorkspace.Domain.Model;

namespace FastWorkspace.Domain.Services.Declarations;

public interface IWorkspaceStore
{
    public Task<ResultWithContent<IEnumerable<Workspace>>> GetAllAsync();

    public Task<ResultWithContent<Workspace>> GetByIdAsync(Guid id);

    public Task<Result> AddOrUpdateAsync(Workspace workspace);

    public Task<Result> DeleteAsync(Workspace workspace);

    public void SetupStore();
}
