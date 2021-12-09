using ProjectAveryCommon.Model.Application;
using ProjectAveryFrontend.Model;

namespace ProjectAveryFrontend.Logic.Services.Managers;

public interface IApplicationStateManager
{
    public delegate void HandleAppStatusChanged();

    public event HandleAppStatusChanged AppStatusChanged;

    public bool IsApplicationReady { get; }
    public State ApplicationState { get; }
    public WebsocketStatus WebsocketStatus { get; set; }
    public ApplicationStatus ApplicationStatus { get; }

    public Task UpdateState();
}