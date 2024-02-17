using System.Text;
using ActivityLauncher.Domain.Common.Utils;
using ActivityLauncher.Domain.Model;
using ActivityLauncher.Domain.Services.Declarations;

namespace ActivityLauncher.Client.Common.Services;

public class ActivitiesStartupService : IActivitiesStartupService
{
    private const string StartupDirectory = "Startup";
    private const string StartupFileName = "ActivityLauncherForWindows_StartupScriptFile.ps1";

    private readonly IFileStorage _fileStorage;

    private readonly IScriptGeneratorService _scriptGenerator;

    public ActivitiesStartupService(IFileStorage fileStorage, IScriptGeneratorService scriptGenerator)
    {
        _fileStorage = fileStorage;
        _scriptGenerator = scriptGenerator;
    }

    public string GetStartupScriptFilePath()
    {
        return Path.Combine(GetStartupStorageDirectoryPath(), StartupFileName);
    }

    public async Task<Result> UpdateActivitiesStartup(IEnumerable<Activity> activities)
    {
        var result = ResetStartupDirectory();
        if (result.Failure) return result;

        var tasks = new List<Task>();

        var activitiesAtStartup = activities.Where(x => x.LaunchAtStartup);

        if (!activitiesAtStartup.Any()) return Result.SuccessResult;

        foreach (var activity in activitiesAtStartup)
        {
            tasks.Add(_fileStorage.WriteTextToFileAsync(GetActivityStartupScriptFilePath(activity), _scriptGenerator.GetScript(activity)));
        }

        tasks.Add(_fileStorage.WriteTextToFileAsync(GetStartupScriptFilePath(), GetStartupScriptFileContent(activitiesAtStartup)));

        try
        {
            await Task.WhenAll(tasks);
            return Result.SuccessResult;
        }
        catch (Exception exception)
        {
            return exception.AsResult();
        }
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

        foreach (var activity in activities)
        {
            builder.AppendLine($".\\{GetActivityStartupScriptFileName(activity)};");
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
