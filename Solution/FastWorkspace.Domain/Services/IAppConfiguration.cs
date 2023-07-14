namespace FastWorkspace.Domain.Services;

public interface IAppConfiguration
{
    public bool IsAppReady();

    public string? GetFileStorageDirectoryPath();

    public bool SetFileStorageDirectoryPath(string? path);

    public bool RemoveFileStorageDirectoryPath();
}
