using FastWorkspace.Client.Common.Events;
using FastWorkspace.Client.Shared.Organisms;
using FastWorkspace.Domain;
using FastWorkspace.Domain.Services;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MudBlazor;

namespace FastWorkspace.Client.Common.Controllers;

public class WorkspaceController : ApplicationController, IDisposable
{
    public delegate Task WorkspacesChangedHandler();

    private readonly IWorkspaceStore _store;

    private readonly List<WorkspacesChangedHandler> _handlers = new();

    public WorkspaceController(ApplicationEventManager eventManager, ISnackbar notificationService, IDialogService dialogService, IStringLocalizer<AppLocales> localize, ILogger<ApplicationController> logger, IWorkspaceStore store)
        : base(eventManager, notificationService, dialogService, localize, logger)
    {
        _store = store;
        _eventManager.OnEvent += EventPublish;
    }

    public async Task<IEnumerable<Workspace>?> GetWorkspaces()
    {
        var result = await _store.GetAllAsync();

        if (result.Success)
        {
            return result.Content!;
        }
        else
        {
            await PublishError(result.Exception!.ToErrorEventDetails(_localize["Errors.GetWorkspaces"]));
            return null;
        }
    }

    public async Task<bool> CreateWorkspace(Workspace workspace)
    {
        workspace.CreationDate = DateTime.Now;
        workspace.LastModifiedDate = workspace.CreationDate;

        var result = await _store.AddOrUpdateAsync(workspace);

        if (result.Success)
        {
            NotifySuccess(_localize["Notifications.CreateWorkspace"]);
            await PublishEvent(ApplicationEventType.WorkspaceCreated, workspace);
            return true;
        }
        else
        {
            await PublishError(result.Exception!.ToErrorEventDetails(_localize["Errors.CreateWorkspace"]));
            return false;
        }
    }

    public async Task<Workspace?> DuplicateWorkspace(Workspace workspace)
    {
        var duplicatedWorkspace = workspace.Clone();

        duplicatedWorkspace.Id = Guid.NewGuid();
        duplicatedWorkspace.Name = string.Format(_localize["Workspace.Duplicate.Name"], workspace.Name);

        duplicatedWorkspace.CreationDate = DateTime.Now;
        duplicatedWorkspace.LastModifiedDate = duplicatedWorkspace.CreationDate;

        var result = await _store.AddOrUpdateAsync(duplicatedWorkspace);

        if (result.Success)
        {
            NotifySuccess(_localize["Notifications.DuplicateWorkspace"]);
            await PublishEvent(ApplicationEventType.WorkspaceCreated, duplicatedWorkspace);
            return duplicatedWorkspace;
        }
        else
        {
            await PublishError(result.Exception!.ToErrorEventDetails(_localize["Errors.DuplicateWorkspace"]));
            return null;
        }
    }

    public async Task<bool> UpdateWorkspace(Workspace workspace)
    {
        workspace.LastModifiedDate = DateTime.Now;

        var result = await _store.AddOrUpdateAsync(workspace);

        if (result.Success)
        {
            NotifySuccess(_localize["Notifications.UpdateWorkspace"]);
            await PublishEvent(ApplicationEventType.WorkspaceUpdated, workspace);
            return true;
        }
        else
        {
            await PublishError(result.Exception!.ToErrorEventDetails(_localize["Errors.UpdateWorkspace"]));
            return false;
        }
    }

    public async Task<bool> DeleteWorkspace(Workspace workspace)
    {
        var result = await _store.DeleteAsync(workspace);

        if (result.Success)
        {
            NotifySuccess(_localize["Notifications.DeleteWorkspace"]);
            await PublishEvent(ApplicationEventType.WorkspaceDeleted, workspace);
            return true;
        }
        else
        {
            await PublishError(result.Exception!.ToErrorEventDetails(_localize["Errors.DeleteWorkspace"]));
            return false;
        }
    }

    public IDialogReference OpenCreateWorkspaceDialog()
    {
        var options = new DialogOptions() { CloseButton = false, CloseOnEscapeKey = true, FullWidth = true };
        return _dialogService.Show<WorkspaceDialog>(_localize["Workspace.Dialog.Create.Title"], options);
    }

    public IDialogReference OpenEditWorkspaceDialog(Workspace workspace)
    {
        var parameters = new DialogParameters<WorkspaceDialog>();
        parameters.Add(x => x.WorkspaceToEdit, workspace);
        var options = new DialogOptions() { CloseButton = false, CloseOnEscapeKey = true, FullWidth = true };
        return _dialogService.Show<WorkspaceDialog>(_localize["Workspace.Dialog.Edit.Title"], parameters, options);
    }

    public async Task<bool> PromptDeleteWorkspace(Workspace workspace)
    {
        bool? result = await _dialogService.ShowMessageBox(
            _localize["Workspace.Dialog.Delete.Title"],
            string.Format(_localize["Workspace.Dialog.Delete.Message"], workspace.Name),
            yesText: _localize["Workspace.Dialog.Delete.ConfirmButton"], cancelText: _localize["Workspace.Dialog.Delete.CancelButton"]);

        if (result.HasValue && result.Value == true)
        {
            return await DeleteWorkspace(workspace);
        }
        else
        {
            return false;
        }
    }

    public async Task<bool> PromptExecuteWorkspaceScript(Workspace workspace)
    {
        // todo: implémenter l'exécution de script
        // 1. demander confirmation à l'utilisateur
        // 2. lancer le script
        return false;
    }

    public IDialogReference OpenWorkspaceScriptDialog(Workspace workspace)
    {
        // todo: implémenter l'ouverture du dialogue de lecture de script
        throw new NotImplementedException();
    }

    private async Task EventPublish(object sender, ApplicationEvent applicationEvent)
    {
        if (applicationEvent.Type is ApplicationEventType.WorkspaceCreated or ApplicationEventType.WorkspaceUpdated or ApplicationEventType.WorkspaceDeleted)
        {
            await Task.WhenAll(_handlers.Select(handler => handler()));
        }
    }

    public void AddWorkspaceChangedListener(WorkspacesChangedHandler handler)
    {
        _handlers.Add(handler);
    }

    public void RemoveWorkspaceChangedListener(WorkspacesChangedHandler handler)
    {
        _handlers.Remove(handler);
    }

    public void Dispose()
    {
        _eventManager.OnEvent -= EventPublish;
    }
}
