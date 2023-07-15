using FastWorkspace.Domain.Enums;
using FastWorkspace.Domain.Services;

namespace FastWorkspace.Client.Common.Services;

public class AppConfiguration : IAppConfiguration
{
    private const string AppDataDirectoryPathConfigKey = "AppDataDirectoryPathConfigKey";
    private const string LanguageConfigKey = "LanguageConfigKey";
    private const string ThemeConfigKey = "ThemeConfigKey";

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

    public Language GetLanguage()
    {
        var value = Preferences.Get(LanguageConfigKey, null);

        if (value != null && int.TryParse(value, out var intValue) && Enum.IsDefined(typeof(Language), intValue))
        {
            return (Language)intValue;
        }

        return Language.English;
    }

    public void SetLanguage(Language language)
    {
        Preferences.Set(LanguageConfigKey, ((int)language).ToString());
    }

    public Theme GetTheme()
    {
        var value = Preferences.Get(ThemeConfigKey, null);

        if (value != null && int.TryParse(value, out var intValue) && Enum.IsDefined(typeof(Theme), intValue))
        {
            return (Theme)intValue;
        }

        return Theme.Light;
    }

    public void SetTheme(Theme theme)
    {
        Preferences.Set(ThemeConfigKey, ((int)theme).ToString());
    }
}
