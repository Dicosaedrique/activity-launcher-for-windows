using FastWorkspace.Domain.Enums;

namespace FastWorkspace.Domain.Services;

public interface IAppConfiguration
{
    public string? GetFileStorageDirectoryPath();

    public bool SetFileStorageDirectoryPath(string? path);

    public Language GetLanguage();

    public void SetLanguage(Language language);

    public Theme GetTheme();

    public void SetTheme(Theme theme);
}
