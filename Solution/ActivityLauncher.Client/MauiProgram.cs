using ActivityLauncher.Client.Common.Controllers;
using ActivityLauncher.Client.Common.Events;
using ActivityLauncher.Client.Common.Services;
using ActivityLauncher.Domain.Services;
using ActivityLauncher.Domain.Services.Declarations;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using MudBlazor;
using MudBlazor.Services;

namespace ActivityLauncher.Client;

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
        builder.Services.AddTransient<IActivityStore, ActivityStore>();
        builder.Services.AddTransient<IActivitiesStartupService, ActivitiesStartupService>();
        builder.Services.AddTransient<IMauiInitializeService, ApplicationInitializeService>();
        builder.Services.AddTransient<IScriptGeneratorService, ScriptGeneratorService>();
        builder.Services.AddTransient<IPowerShellScriptRunner, PowerShellScriptRunner>();
        builder.Services.AddTransient<InteropService>();
        builder.Services.AddTransient<HighlighterService>();

        // add controllers
        builder.Services.AddTransient<ApplicationController>();
        builder.Services.AddTransient<ActivityController>();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
