using CommunityToolkit.Maui;
using FastWorkspace.Client.Common.Events;
using FastWorkspace.Client.Common.Services;
using FastWorkspace.Domain.Services;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace FastWorkspace.Client;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder.UseMauiApp<App>().UseMauiCommunityToolkit();
        builder.Services.AddMauiBlazorWebView();

        builder.Services.AddMudServices();

        builder.Services.AddSingleton<ApplicationEventManager>();
        builder.Services.AddSingleton<IAppConfiguration, AppConfiguration>();
        builder.Services.AddSingleton<IFileStorage, DataFileStore>();
        builder.Services.AddSingleton<IWorkspaceStore, WorkspaceStore>();

        builder.Services.AddTransient<IMauiInitializeService, AppInitializeService>();

        builder.Services.AddLocalization();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
