using ActivityLauncher.Domain.Common.Localization;
using ActivityLauncher.Domain.Services.Declarations;
using System.Globalization;

namespace ActivityLauncher.Client.Core.Services;

public class ApplicationInitializeService : IMauiInitializeService
{
    private IAppConfiguration _appConfiguration = null!;
    private IActivityStore _activityStore = null!;
    private IActivitiesStartupService _startupService = null!;

    public void Initialize(IServiceProvider services)
    {
        _appConfiguration = services.GetRequiredService<IAppConfiguration>();
        _activityStore = services.GetRequiredService<IActivityStore>();
        _startupService = services.GetRequiredService<IActivitiesStartupService>();

        SetupData();
        SetupApplicationLanguage();
    }

    private void SetupData()
    {
        if (_appConfiguration.GetFileStorageDirectoryPath() != null)
        {
            var setupStoreresult = _activityStore.SetupStore();
            setupStoreresult.Ensure();

            var createStartupFileresult = _startupService.CreateStartupFileDemo();
            createStartupFileresult.Ensure();
        }
    }

    private void SetupApplicationLanguage()
    {
        var language = _appConfiguration.GetLanguage();

        CultureInfo.CurrentCulture = language.GetCulture();
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CurrentCulture;
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.CurrentCulture;
    }
}
