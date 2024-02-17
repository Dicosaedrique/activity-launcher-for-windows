using ActivityLauncher.Domain.Common.Utils;
using ActivityLauncher.Domain.Model;

namespace ActivityLauncher.Domain.Services.Declarations;

public interface IActivitiesStartupService
{
    public Task<Result> UpdateActivitiesStartup(IEnumerable<Activity> activities);

    public string GetStartupScriptFilePath();
}
