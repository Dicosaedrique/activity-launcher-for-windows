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
        builder.Services.AddLocalization();

        builder.Services.AddMudServices(config =>
        {
            config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomEnd;
            config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
            config.SnackbarConfiguration.PreventDuplicates = false;
            config.SnackbarConfiguration.ShowCloseIcon = true;
            config.SnackbarConfiguration.VisibleStateDuration = 10000;
            config.SnackbarConfiguration.HideTransitionDuration = 200;
            config.SnackbarConfiguration.ShowTransitionDuration = 200;

        });

        // add application event
        builder.Services.AddSingleton<ApplicationEventManager>();

        // add services
        builder.Services.AddTransient<IAppConfiguration, AppConfiguration>();
        builder.Services.AddTransient<IFileStorage, DataFileStore>();
        builder.Services.AddTransient<IWorkspaceStore, WorkspaceStore>();
        builder.Services.AddTransient<IMauiInitializeService, ApplicationInitializeService>();
        builder.Services.AddTransient<InteropService>();
        builder.Services.AddTransient<HighlighterService>();

        // add controllers
        builder.Services.AddTransient<ApplicationController>();
        builder.Services.AddTransient<WorkspaceController>();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
