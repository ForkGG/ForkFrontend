using ProjectAveryCommon.Model.Application;

namespace ProjectAveryFrontend.Logic.Services;

public interface IApplicationConnectionService
{
    public Task<State> GetApplicationState();
}