using ProjectAveryCommon.Model.Application;

namespace ProjectAveryFrontend.Logic.Services.Connections;

public interface IApplicationConnectionService
{
    public Task<State> GetApplicationState();
}