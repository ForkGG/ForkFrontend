using Microsoft.AspNetCore.Components;
using ProjectAveryCommon.Model.Application;
using ProjectAveryCommon.Model.Entity.Pocos;
using ProjectAveryFrontend.Logic.Services.Connections;

namespace ProjectAveryFrontend.Shared.Components.Sidebar;

public partial class Sidebar : ComponentBase
{
    public Sidebar()
    {
        Entities = new List<IEntity>();
    }

    [Inject] private IApplicationConnectionService ApplicationConnection { get; set; }

    private List<IEntity> Entities { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await Task.Delay(1000);
        State state = await ApplicationConnection.GetApplicationState();
        Entities = new List<IEntity>(state.Entities);
        StateHasChanged();
    }
}