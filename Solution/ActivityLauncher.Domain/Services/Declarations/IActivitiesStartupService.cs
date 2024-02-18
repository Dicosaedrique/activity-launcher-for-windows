using ActivityLauncher.Domain.Common.Utils;
using ActivityLauncher.Domain.Model;

namespace ActivityLauncher.Domain.Services.Declarations;

public interface IActivitiesStartupService
{
    public Result UpdateActivitiesStartup(IEnumerable<Activity> activities);

    public string GetStartupScriptFilePath();

    public string GetStartupFilePath();

    public string GetDemoStartupDirectoryPath();

    public Result CreateStartupFileDemo();
}
