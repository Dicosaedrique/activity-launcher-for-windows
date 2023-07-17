using FastWorkspace.Client.Common.Events;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MudBlazor;

namespace FastWorkspace.Client.Common.Controllers;

public class ApplicationController
{
    protected readonly ApplicationEventManager _eventManager;

    protected readonly ISnackbar _notificationService;

    protected readonly IDialogService _dialogService;

    protected readonly IStringLocalizer<AppLocales> _localize;

    protected readonly ILogger<ApplicationController> _logger;

    public ApplicationController(ApplicationEventManager eventManager, ISnackbar notificationService, IDialogService dialogService, IStringLocalizer<AppLocales> localize, ILogger<ApplicationController> logger)
    {
        _eventManager = eventManager;
        _notificationService = notificationService;
        _dialogService = dialogService;
        _localize = localize;
        _logger = logger;
    }

    protected async Task PublishEvent(ApplicationEventType eventType, object? data = null)
    {
        await _eventManager.PublishEvent(this, eventType, data);
    }

    protected async Task PublishError(ErrorEventDetails details)
    {
        // todo: ajouter une gestion des erreurs par snackbar
        await _eventManager.PublishError(this, details);
    }

    public void Notify(string title, Severity severity = Severity.Normal)
    {
        _notificationService.Add(title, severity);
    }

    public void NotifySuccess(string title)
    {
        Notify(title, Severity.Success);
    }

    public void NotifyError(string title)
    {
        Notify(title, Severity.Error);
    }
}
