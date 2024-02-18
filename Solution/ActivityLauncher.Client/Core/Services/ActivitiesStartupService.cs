using System.Text;
using ActivityLauncher.Client.Locales;
using ActivityLauncher.Domain.Common.Utils;
using ActivityLauncher.Domain.Model;
using ActivityLauncher.Domain.Services.Declarations;
using Microsoft.Extensions.Localization;

namespace ActivityLauncher.Client.Core.Services;

public class ActivitiesStartupService : IActivitiesStartupService
{
    private const string StartupDirectory = "Startup";
    private const string StartupScriptFileName = "Startup.ps1";
    private const string StartupDemoFileName = "ActivityLauncherForWindowsStartup.cmd";

    private readonly IFileStorage _fileStorage;

    private readonly IScriptGeneratorService _scriptGenerator;

    private readonly IStringLocalizer<CommonLocales> _localize;

    public ActivitiesStartupService(IFileStorage fileStorage, IScriptGeneratorService scriptGenerator, IStringLocalizer<CommonLocales> localize)
    {
        _fileStorage = fileStorage;
        _scriptGenerator = scriptGenerator;
        _localize = localize;
    }

    public string GetStartupScriptFilePath()
    {
        return Path.Combine(GetStartupStorageDirectoryPath(), StartupScriptFileName);
    }

    public string GetStartupFilePath()
    {
        return _fileStorage.GetStorageFilePath(StartupDemoFileName);
    }

    public string GetDemoStartupDirectoryPath()
    {
        return "C:\\Users\\{YOUR_USER}\\AppData\\Roaming\\Microsoft\\Windows\\Start Menu\\Programs\\Startup";
    }

    public Result UpdateActivitiesStartup(IEnumerable<Activity> activities)
    {
        var result = ResetStartupDirectory();
        if (result.Failure) return result;

        var activitiesAtStartup = activities.Where(x => x.LaunchAtStartup);

        if (!activitiesAtStartup.Any()) return Result.SuccessResult;

        var results = new List<Result>();

        foreach (var activity in activitiesAtStartup)
        {
            results.Add(_fileStorage.WriteTextToFile(GetActivityStartupScriptFilePath(activity), _scriptGenerator.GetScript(activity)));
        }

        results.Add(_fileStorage.WriteTextToFile(GetStartupScriptFilePath(), GetStartupScriptFileContent(activitiesAtStartup)));

        if (results.All(x => x.Success))
        {
            return Result.SuccessResult;
        }
        else
        {
            return new AggregateException(results.Where(x => x.Failure).Select(x => x.Exception!)).AsResult();
        }
    }

    public Result CreateStartupFileDemo()
    {
        var builder = new StringBuilder();

        var startupScriptFilePath = GetStartupScriptFilePath();

        builder.AppendLine($":: {_localize["StartupFile.FileDescription"]}");
        builder.AppendLine($":: {_localize["StartupFile.HelperText", GetDemoStartupDirectoryPath()]}\n");
        builder.AppendLine($"IF EXIST {startupScriptFilePath} powerShell -windowstyle hidden {startupScriptFilePath}");

        return _fileStorage.WriteTextToFile(GetStartupFilePath(), builder.ToString());
    }

    private Result ResetStartupDirectory()
    {
        var deleteResult = _fileStorage.DeleteDirectory(StartupDirectory);
        if (deleteResult.Failure) return deleteResult;

        var createResult = _fileStorage.CreateDirectory(StartupDirectory);
        if (createResult.Failure) return createResult;

        return Result.SuccessResult;
    }

    private string GetStartupScriptFileContent(IEnumerable<Activity> activities)
    {
        var builder = new StringBuilder();

        builder.AppendLine($"# {_localize["StartupScriptFile.FileDescription"]}");
        builder.AppendLine($"# {_localize["StartupScriptFile.HelpText"]}\n");

        foreach (var activity in activities)
        {
            builder.AppendLine($"powerShell -windowstyle hidden {GetActivityStartupScriptFilePath(activity)};");
        }

        return builder.ToString();
    }

    private string GetStartupStorageDirectoryPath()
    {
        return _fileStorage.GetStorageFilePath(StartupDirectory);
    }

    private string GetActivityStartupScriptFilePath(Activity activity)
    {
        return Path.Combine(GetStartupStorageDirectoryPath(), GetActivityStartupScriptFileName(activity));
    }

    private static string GetActivityStartupScriptFileName(Activity activity) => $"{activity.Id}.ps1";
}
