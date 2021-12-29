using Microsoft.AspNetCore.Components;
using ForkCommon.Model.Entity.Pocos;
using ForkFrontend.Logic.Services.Connections;
using ForkFrontend.Logic.Services.Managers;
using ForkFrontend.Logic.Services.Notifications;

namespace ForkFrontend.Pages;

// This Page controls the whole entity logic
public partial class Index : ComponentBase
{
    [Inject] private IApplicationConnectionService ApplicationConnection { get; set; }
    [Inject] private IApplicationStateManager ApplicationStateManager { get; set; }
    [Inject] private INotificationService NotificationService { get; set; }

    public IEntity? SelectedEntity { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _applicationState.AppStatusChanged += StateHasChanged;
        _applicationState.AppStateChanged += () =>
        {
            if (SelectedEntity != null &&
                _applicationState.ApplicationState.Entities.Any(e => e.Id == SelectedEntity.Id))
                SelectedEntity = _applicationState.ApplicationState.Entities.First(e => e.Id == SelectedEntity.Id);
            else
                SelectedEntity = _applicationState.ApplicationState.Entities.FirstOrDefault();
            StateHasChanged();
        };
        await NotificationService.StartupAsync();
    }

    private async Task OnSelectEntity(IEntity entity)
    {
        SelectedEntity = entity;
    }
}