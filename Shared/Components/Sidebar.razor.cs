using Microsoft.AspNetCore.Components;
using ProjectAveryCommon.Model.Application;
using ProjectAveryCommon.Model.Entity.Pocos;
using ProjectAveryFrontend.Logic.Services;

namespace ProjectAveryFrontend.Shared.Components;

public partial class Sidebar : ComponentBase
{
    public Sidebar()
    {
        Entities = new List<IEntity>();

        Task.Run(async () =>
        {
            await Task.Delay(1000);
            State state = await _applicationConnection.GetApplicationState();
            Entities = new List<IEntity>(state.Entities);
            StateHasChanged();
        });
    }

    [Inject] private IApplicationConnectionService _applicationConnection { get; set; }

    private List<IEntity> Entities { get; set; }
}