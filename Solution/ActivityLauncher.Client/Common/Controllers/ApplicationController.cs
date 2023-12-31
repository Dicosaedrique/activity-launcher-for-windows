﻿using ActivityLauncher.Client.Common.Events;
using ActivityLauncher.Client.Locales;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MudBlazor;

namespace ActivityLauncher.Client.Common.Controllers;

public class ApplicationController
{
    protected readonly ApplicationEventManager _eventManager;

    protected readonly ISnackbar _notificationService;

    protected readonly IDialogService _dialogService;

    protected readonly IStringLocalizer<CommonLocales> _commonLocalize;

    protected readonly ILogger<ApplicationController> _logger;

    public ApplicationController(ApplicationEventManager eventManager, ISnackbar notificationService, IDialogService dialogService, IStringLocalizer<CommonLocales> commonLocalize, ILogger<ApplicationController> logger)
    {
        _eventManager = eventManager;
        _notificationService = notificationService;
        _dialogService = dialogService;
        _commonLocalize = commonLocalize;
        _logger = logger;
    }

    protected async Task PublishEvent(ApplicationEventType eventType, object? data = null)
    {
        await _eventManager.PublishEvent(this, eventType, data);
    }

    protected async Task PublishError(ErrorEventDetails details)
    {
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
