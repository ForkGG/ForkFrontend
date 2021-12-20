using ProjectAveryCommon.Model.Payloads.Entity;

namespace ProjectAveryFrontend.Logic.Services.Connections;

public interface IEntityConnectionService
{
    public Task<bool> SubmitConsoleInAsync(string message, ulong entityId);
    public Task<ulong> CreateServerAsync(CreateServerPayload payload);
    public Task<bool> StartEntityAsync(ulong entityId);
}