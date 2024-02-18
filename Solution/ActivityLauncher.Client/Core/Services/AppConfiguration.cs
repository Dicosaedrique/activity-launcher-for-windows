using System.Globalization;
using ActivityLauncher.Domain.Common.Localization;
using ActivityLauncher.Domain.Enums;
using ActivityLauncher.Domain.Services.Declarations;

namespace ActivityLauncher.Client.Core.Services;

public class AppConfiguration : IAppConfiguration
{
    private const string AppDataDirectoryPathConfigKey = "AppDataDirectoryPathConfigKey";
    private const string LanguageConfigKey = "LanguageConfigKey";
    private const string ThemeConfigKey = "ThemeConfigKey";
    private const string ShowActivityStartupInfoDialogConfigKey = "ShowActivityStartupInfoDialogConfigKey";

    public bool IsAppReady()
    {
        return GetFileStorageDirectoryPath() != null;
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

    public Language GetLanguage()
    {
        var value = Preferences.Get(LanguageConfigKey, null);

        if (value != null && int.TryParse(value, out var intValue) && Enum.IsDefined(typeof(Language), intValue))
        {
            return (Language)intValue;
        }

        return CultureNames.DefaultLanguage;
    }

    public void SetLanguage(Language language)
    {
        Preferences.Set(LanguageConfigKey, ((int)language).ToString());

        CultureInfo.CurrentCulture = language.GetCulture();
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CurrentCulture;
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.CurrentCulture;
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

    public bool GetShowActivityStartupInfoDialog()
    {
        var value = Preferences.Get(ShowActivityStartupInfoDialogConfigKey, bool.TrueString);
        return bool.Parse(value);
    }

    public void SetShowActivityStartupInfoDialog(bool value)
    {
        Preferences.Set(ShowActivityStartupInfoDialogConfigKey, value.ToString());
    }

    public void Reset()
    {
        Preferences.Remove(AppDataDirectoryPathConfigKey);
        Preferences.Remove(LanguageConfigKey);
        Preferences.Remove(ThemeConfigKey);
    }
}
