using ActivityLauncher.Client.Common.Events;
using ActivityLauncher.Client.Locales;
using ActivityLauncher.Client.Pages.Activities.Organisms;
using ActivityLauncher.Domain.Interfaces;
using ActivityLauncher.Domain.Model;
using ActivityLauncher.Domain.Services.Declarations;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MudBlazor;

namespace ActivityLauncher.Client.Common.Controllers;

public class ActivityController : ApplicationController, IDisposable
{
    public delegate Task ActivitiesChangedHandler();

    private readonly IActivityStore _store;

    private readonly IPowerShellScriptRunner _powerShellScriptRunner;

    private readonly IStringLocalizer<ActivityLocales> _localize;

    private readonly List<ActivitiesChangedHandler> _activitiesChangedHandlers = new();

    public ActivityController(ApplicationEventManager eventManager, ISnackbar notificationService, IDialogService dialogService, IStringLocalizer<CommonLocales> commonLocalize, ILogger<ApplicationController> logger, IActivityStore store, IPowerShellScriptRunner powerShellScriptRunner, IStringLocalizer<ActivityLocales> localize)
        : base(eventManager, notificationService, dialogService, commonLocalize, logger)
    {
        _store = store;
        _powerShellScriptRunner = powerShellScriptRunner;
        _localize = localize;

        _eventManager.OnEvent += EventPublish;
    }

    public async Task<IEnumerable<Activity>?> GetActivities()
    {
        var result = await _store.GetAllAsync();

        if (result.Success)
        {
            return result.Content!;
        }
        else
        {
            await PublishError(result.Exception!.ToErrorEventDetails(_localize["Notifications.Errors.GetActivities"]));
            return null;
        }
    }

    public async Task<bool> CreateActivity(Activity activity)
    {
        activity.CreationDate = DateTime.Now;
        activity.LastModifiedDate = activity.CreationDate;

        var result = await _store.AddOrUpdateAsync(activity);

        if (result.Success)
        {
            NotifySuccess(_localize["Notifications.Success.CreateActivity"]);
            await PublishEvent(ApplicationEventType.ActivityCreated, activity);
            return true;
        }
        else
        {
            await PublishError(result.Exception!.ToErrorEventDetails(_localize["Notifications.Errors.CreateActivity"]));
            return false;
        }
    }

    public async Task<Activity?> DuplicateActivity(Activity activity)
    {
        var duplicatedActivity = activity.Clone();

        duplicatedActivity.Id = Guid.NewGuid();
        duplicatedActivity.Name = string.Format(_localize["Activity.Duplicate.Name"], activity.Name);

        duplicatedActivity.CreationDate = DateTime.Now;
        duplicatedActivity.LastModifiedDate = duplicatedActivity.CreationDate;

        var result = await _store.AddOrUpdateAsync(duplicatedActivity);

        if (result.Success)
        {
            NotifySuccess(_localize["Notifications.Success.DuplicateActivity"]);
            await PublishEvent(ApplicationEventType.ActivityCreated, duplicatedActivity);
            return duplicatedActivity;
        }
        else
        {
            await PublishError(result.Exception!.ToErrorEventDetails(_localize["Notifications.Errors.DuplicateActivity"]));
            return null;
        }
    }

    public async Task<bool> UpdateActivity(Activity activity)
    {
        activity.LastModifiedDate = DateTime.Now;

        var result = await _store.AddOrUpdateAsync(activity);

        if (result.Success)
        {
            NotifySuccess(_localize["Notifications.Success.UpdateActivity"]);
            await PublishEvent(ApplicationEventType.ActivityUpdated, activity);
            return true;
        }
        else
        {
            await PublishError(result.Exception!.ToErrorEventDetails(_localize["Notifications.Errors.UpdateActivity"]));
            return false;
        }
    }

    public async Task<bool> DeleteActivity(Activity activity)
    {
        var result = await _store.DeleteAsync(activity);

        if (result.Success)
        {
            NotifySuccess(_localize["Notifications.Success.DeleteActivity"]);
            await PublishEvent(ApplicationEventType.ActivityDeleted, activity);
            return true;
        }
        else
        {
            await PublishError(result.Exception!.ToErrorEventDetails(_localize["Notifications.Errors.DeleteActivity"]));
            return false;
        }
    }

    public IDialogReference OpenActivityDialog(Activity activity)
    {
        var parameters = new DialogParameters<ActivityDialog>();
        parameters.Add(x => x.Activity, activity);
        var options = new DialogOptions() { CloseButton = true, CloseOnEscapeKey = true, FullWidth = true, FullScreen = true };
        return _dialogService.Show<ActivityDialog>(activity.Name, parameters, options);
    }

    public IDialogReference OpenCreateActivityDialog()
    {
        var options = new DialogOptions() { CloseButton = true, CloseOnEscapeKey = true, FullWidth = true, FullScreen = true };
        return _dialogService.Show<ActivityDialogEdit>(_localize["Activity.Dialog.Create.Title"], options);
    }

    public IDialogReference OpenEditActivityDialog(Activity activity)
    {
        var parameters = new DialogParameters<ActivityDialogEdit>();
        parameters.Add(x => x.ActivityToEdit, activity);
        var options = new DialogOptions() { CloseButton = true, CloseOnEscapeKey = true, FullWidth = true, FullScreen = true };
        return _dialogService.Show<ActivityDialogEdit>(_localize["Activity.Dialog.Edit.Title"], parameters, options);
    }

    public async Task<bool> PromptDeleteActivity(Activity activity)
    {
        bool? result = await _dialogService.ShowMessageBox(
            _localize["Activity.Dialog.Delete.Title"],
            string.Format(_localize["Activity.Dialog.Delete.Message"], activity.Name),
            yesText: _localize["Activity.Dialog.Delete.ConfirmButton"],
            cancelText: _localize["Activity.Dialog.Delete.CancelButton"]);

        if (result.HasValue && result.Value)
        {
            return await DeleteActivity(activity);
        }
        else
        {
            return false;
        }
    }

    public async Task<bool> PromptLaunchActivity(Activity activity, bool showReviewScriptOption)
    {
        bool? result = await _dialogService.ShowMessageBox(
            string.Format(_localize["Activity.Dialog.Launch.Title"], activity.Name),
            string.Format(_localize["Activity.Dialog.Launch.Message"], activity.Name),
            yesText: _localize["Activity.Dialog.Launch.ConfirmButton"],
            noText: showReviewScriptOption ? _localize["Activity.Dialog.Launch.ReviewScript"].Value : null,
            cancelText: _localize["Activity.Dialog.Launch.CancelButton"]);

        if (result.HasValue && result.Value)
        {
            return await LaunchActivity(activity);
        }
        else if (result.HasValue)
        {
            OpenActivityScriptDialog(activity);
            return false;
        }
        else
        {
            return false;
        }
    }

    public async Task<bool> PromptLaunchTask(ITask task)
    {
        bool? result = await _dialogService.ShowMessageBox(
            string.Format(_localize["Task.Dialog.Launch.Title"], task.Name ?? _localize["Task.EmptyName"]),
            string.Format(_localize["Task.Dialog.Launch.Message"], task.Name ?? _localize["Task.EmptyName"]),
            yesText: _localize["Task.Dialog.Launch.ConfirmButton"],
            cancelText: _localize["Task.Dialog.Launch.CancelButton"]);

        if (result.HasValue && result.Value)
        {
            return await LaunchTask(task);
        }
        else
        {
            return false;
        }
    }

    public async Task<bool> LaunchActivity(Activity activity)
    {
        var result = await _powerShellScriptRunner.RunScript(activity);

        if (result.Success)
        {
            NotifySuccess(_localize["Notifications.Success.LaunchActivity"]);
            return true;
        }
        else
        {
            await PublishError(result.Exception!.ToErrorEventDetails(_localize["Notifications.Errors.LaunchActivity"]));
            return false;
        }
    }

    public async Task<bool> LaunchTask(ITask task)
    {
        var result = await _powerShellScriptRunner.RunScript(task);

        if (result.Success)
        {
            NotifySuccess(_localize["Notifications.Success.LaunchTask"]);
            return true;
        }
        else
        {
            await PublishError(result.Exception!.ToErrorEventDetails(_localize["Notifications.Errors.LaunchTask"]));
            return false;
        }
    }

    public IDialogReference OpenActivityScriptDialog(Activity activity)
    {
        var parameters = new DialogParameters<ActivityScriptDialog>();
        parameters.Add(x => x.Activity, activity);
        var options = new DialogOptions() { CloseButton = true, CloseOnEscapeKey = true, FullWidth = true, FullScreen = true };
        return _dialogService.Show<ActivityScriptDialog>(activity.Name, parameters, options);
    }

    private async Task EventPublish(object sender, ApplicationEvent applicationEvent)
    {
        if (applicationEvent.Type is ApplicationEventType.ActivityCreated or ApplicationEventType.ActivityUpdated or ApplicationEventType.ActivityDeleted)
        {
            await Task.WhenAll(_activitiesChangedHandlers.Select(handler => handler()));
        }
    }

    public void AddActivityChangedListener(ActivitiesChangedHandler handler)
    {
        _activitiesChangedHandlers.Add(handler);
    }

    public void RemoveActivityChangedListener(ActivitiesChangedHandler handler)
    {
        _activitiesChangedHandlers.Remove(handler);
    }

    public void Dispose()
    {
        _eventManager.OnEvent -= EventPublish;
    }
}
