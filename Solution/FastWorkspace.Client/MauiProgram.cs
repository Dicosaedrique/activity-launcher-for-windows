using CommunityToolkit.Maui;
using FastWorkspace.Client.Common.Services;
using FastWorkspace.Domain.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Fast.Components.FluentUI;
using Microsoft.Fast.Components.FluentUI.Infrastructure;

namespace FastWorkspace.Client;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder.UseMauiApp<App>().UseMauiCommunityToolkit();
        builder.Services.AddMauiBlazorWebView();

        builder.Services.AddFluentUIComponents(options =>
        {
            options.HostingModel = BlazorHostingModel.Hybrid;
            options.IconConfiguration = ConfigurationGenerator.GetIconConfiguration();
            options.EmojiConfiguration = ConfigurationGenerator.GetEmojiConfiguration();
        });

        builder.Services.AddSingleton<IStaticAssetService, FileBasedStaticAssetService>();

        builder.Services.AddSingleton<IAppConfiguration, AppConfiguration>();
        builder.Services.AddSingleton<IFileStorage, DataFileStore>();
        builder.Services.AddSingleton<IWorkspaceStore, WorkspaceStore>();

        Preferences.Remove("AppDataDirectoryPathConfigKey"); // temp

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
