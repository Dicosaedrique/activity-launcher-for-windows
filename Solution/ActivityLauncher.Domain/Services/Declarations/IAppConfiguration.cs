using ActivityLauncher.Domain.Enums;

namespace ActivityLauncher.Domain.Services.Declarations;

public interface IAppConfiguration
{
    public bool IsAppReady();

    public string? GetFileStorageDirectoryPath();

    public bool SetFileStorageDirectoryPath(string? path);

    public Language GetLanguage();

    public void SetLanguage(Language language);

    public Theme GetTheme();

    public void SetTheme(Theme theme);

    public void Reset();
}
