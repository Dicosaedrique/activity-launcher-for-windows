using ActivityLauncher.Domain.Common.Utils;
using ActivityLauncher.Domain.Model;

namespace ActivityLauncher.Domain.Services.Declarations;

public interface IActivityStore
{
    public Task<ResultWithContent<IEnumerable<Activity>>> GetAllAsync();

    public Task<ResultWithContent<Activity>> GetByIdAsync(Guid id);

    public Task<Result> AddOrUpdateAsync(Activity activity);

    public Task<Result> DeleteAsync(Activity activity);

    public void SetupStore();
}
