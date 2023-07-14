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

        builder.UseMauiApp<App>();
        builder.Services.AddMauiBlazorWebView();

        builder.Services.AddMudServices();

        builder.Services.AddSingleton<IAppConfiguration, AppConfiguration>();
        builder.Services.AddSingleton<IFileStorage, DataFileStore>();
        builder.Services.AddSingleton<IWorkspaceStore, WorkspaceStore>();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
