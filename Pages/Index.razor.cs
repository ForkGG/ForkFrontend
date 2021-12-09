using Microsoft.AspNetCore.Components;
using ProjectAveryCommon.Model.Application;
using ProjectAveryCommon.Model.Entity.Pocos;
using ProjectAveryFrontend.Logic.Services.Connections;
using ProjectAveryFrontend.Logic.Services.Notifications;

namespace ProjectAveryFrontend.Pages;

// This Page controls the whole entity logic
public partial class Index : ComponentBase
{
    public Index()
    {
        Entities = new List<IEntity>();
    }

    [Inject] private IApplicationConnectionService ApplicationConnection { get; set; }
    [Inject] private INotificationService NotificationService { get; set; }

    public IEntity? SelectedEntity { get; set; }

    private List<IEntity> Entities { get; set; }

    protected override async Task OnInitializedAsync()
    {
        //To simulate a network delay
        //await Task.Delay(1000);
        //TODO handle not available error and display it correctly
        State state = await ApplicationConnection.GetApplicationState();
        Entities = new List<IEntity>(state.Entities);
        SelectedEntity = Entities.FirstOrDefault();
        StateHasChanged();
        _ = NotificationService.StartupAsync();
    }

    private async Task OnSelectEntity(IEntity entity)
    {
        SelectedEntity = entity;
    }
}