using ActivityLauncher.Domain.Common.Utils;
using ActivityLauncher.Domain.Model;

namespace ActivityLauncher.Domain.Services.Declarations;

public interface IActivityStore
{
    public ResultWithContent<IEnumerable<Activity>> GetAll();

    public ResultWithContent<Activity> GetById(Guid id);

    public Result AddOrUpdate(Activity activity);

    public Result Delete(Activity activity);

    public Result SetupStore();
}
