using CommunityToolkit.Maui;
using FastWorkspace.Client.Common.Controllers;
using FastWorkspace.Client.Common.Events;
using FastWorkspace.Client.Common.Services;
using FastWorkspace.Domain.Services;
using Microsoft.Extensions.Logging;
using MudBlazor;
using MudBlazor.Services;

namespace FastWorkspace.Client;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder.UseMauiApp<App>().UseMauiCommunityToolkit();
        builder.Services.AddMauiBlazorWebView();

        builder.Services.AddMudServices(config =>
        {
            config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopEnd;
            config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
        });

        builder.Services.AddSingleton<ApplicationEventManager>();
        builder.Services.AddSingleton<IAppConfiguration, AppConfiguration>();
        builder.Services.AddSingleton<IFileStorage, DataFileStore>();
        builder.Services.AddSingleton<IWorkspaceStore, WorkspaceStore>();
        builder.Services.AddSingleton<ApplicationController>();
        builder.Services.AddSingleton<WorkspaceController>();

        builder.Services.AddTransient<IMauiInitializeService, ApplicationInitializeService>();

        builder.Services.AddLocalization();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
