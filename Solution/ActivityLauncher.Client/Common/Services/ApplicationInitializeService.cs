using System.Globalization;
using ActivityLauncher.Domain.Common.Localization;
using ActivityLauncher.Domain.Services.Declarations;

namespace ActivityLauncher.Client.Common.Services;

public class ApplicationInitializeService : IMauiInitializeService
{
    private IAppConfiguration _appConfiguration = null!;
    private IWorkspaceStore _workspaceStore = null!;

    public void Initialize(IServiceProvider services)
    {
        _appConfiguration = services.GetRequiredService<IAppConfiguration>();
        _workspaceStore = services.GetRequiredService<IWorkspaceStore>();

        SetupStores();
        SetupApplicationLanguage();
    }

    private void SetupStores()
    {
        if (_appConfiguration.GetFileStorageDirectoryPath() != null)
        {
            _workspaceStore.SetupStore();
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
