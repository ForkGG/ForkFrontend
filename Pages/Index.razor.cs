using Microsoft.AspNetCore.Components;
using ProjectAveryCommon.Model.Entity.Pocos;
using ProjectAveryFrontend.Logic.Services.Connections;
using ProjectAveryFrontend.Logic.Services.Managers;
using ProjectAveryFrontend.Logic.Services.Notifications;

namespace ProjectAveryFrontend.Pages;

// This Page controls the whole entity logic
public partial class Index : ComponentBase
{
    [Inject] private IApplicationConnectionService ApplicationConnection { get; set; }
    [Inject] private IApplicationStateManager ApplicationStateManager { get; set; }
    [Inject] private INotificationService NotificationService { get; set; }

    public IEntity? SelectedEntity { get; set; }

    protected override async Task OnInitializedAsync()
    {
        //To simulate a network delay
        //await Task.Delay(1000);
        _applicationState.AppStatusChanged += StateHasChanged;
        await _applicationState.UpdateState();
        SelectedEntity = _applicationState.ApplicationState.Entities.FirstOrDefault();
        _ = NotificationService.StartupAsync();
    }

    private async Task OnSelectEntity(IEntity entity)
    {
        SelectedEntity = entity;
    }
}