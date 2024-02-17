using ActivityLauncher.Domain.Common.Utils;
using ActivityLauncher.Domain.Model;
using ActivityLauncher.Domain.Services.Declarations;

namespace ActivityLauncher.Domain.Services;

public class ActivityStore : IActivityStore
{
    private const string ActivityDirectory = "Activities";

    private readonly IFileStorage _fileStorage;

    public ActivityStore(IFileStorage fileStorage)
    {
        _fileStorage = fileStorage;
    }

    public ResultWithContent<IEnumerable<Activity>> GetAll()
    {
        var errors = new List<Exception>();
        var activities = new List<Activity>();

        foreach (var activityFilePath in GetActivityFilePaths())
        {
            var result = _fileStorage.ReadFile<Activity>(activityFilePath);

            if (result.Success)
            {
                activities.Add(result.Content!);
            }
            else
            {
                errors.Add(result.Exception!);
            }
        }

        if (errors.Any())
        {
            return new ResultWithContent<IEnumerable<Activity>>(new AggregateException(errors));
        }

        return new ResultWithContent<IEnumerable<Activity>>(activities);

    }

    public ResultWithContent<Activity> GetById(Guid id)
    {
        return _fileStorage.ReadFile<Activity>(GetActivityFilePathById(id));
    }

    public Result AddOrUpdate(Activity activity)
    {
        return _fileStorage.WriteFile(GetActivityFilePath(activity), activity);
    }

    public Result Delete(Activity activity)
    {
        return _fileStorage.DeleteFile(GetActivityFilePath(activity));
    }

    public Result SetupStore()
    {
        return _fileStorage.CreateDirectory(ActivityDirectory);
    }

    private string GetActivityStorageDirectoryPath()
    {
        return _fileStorage.GetStorageFilePath(ActivityDirectory);
    }

    private IEnumerable<string> GetActivityFilePaths()
    {
        return _fileStorage.GetFiles(GetActivityStorageDirectoryPath(), "*.json");
    }

    private string GetActivityFilePath(Activity activity)
    {
        return Path.Combine(GetActivityStorageDirectoryPath(), GetActivityFileName(activity));
    }

    private string GetActivityFilePathById(Guid id)
    {
        return Path.Combine(GetActivityStorageDirectoryPath(), GetActivityFileNameById(id));
    }

    private static string GetActivityFileName(Activity activity) => GetActivityFileNameById(activity.Id);

    private static string GetActivityFileNameById(Guid id) => $"{id}.json";
}
