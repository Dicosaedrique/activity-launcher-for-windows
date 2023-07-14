using FastWorkspace.Domain.Services;

namespace FastWorkspace.Client.Common.Services;

public class AppConfiguration : IAppConfiguration
{
    private const string AppDataDirectoryPathConfigKey = "AppDataDirectoryPathConfigKey";

    public bool IsAppReady()
    {
        return GetFileStorageDirectoryPath() != null; // todo: add other configuration validation here
    }

    public string? GetFileStorageDirectoryPath()
    {
        return Preferences.Get(AppDataDirectoryPathConfigKey, null);
    }

    public bool SetFileStorageDirectoryPath(string? path)
    {
        if (!string.IsNullOrWhiteSpace(path) && Directory.Exists(path))
        {
            Preferences.Set(AppDataDirectoryPathConfigKey, path);
            return true;
        }

        return false;
    }

    public bool RemoveFileStorageDirectoryPath()
    {
        var exist = Preferences.ContainsKey(AppDataDirectoryPathConfigKey);
        Preferences.Remove(AppDataDirectoryPathConfigKey);
        return exist;
    }
}
