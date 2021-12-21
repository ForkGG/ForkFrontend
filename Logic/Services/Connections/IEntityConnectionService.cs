using ProjectAveryCommon.Model.Entity.Transient.Console;
using ProjectAveryCommon.Model.Payloads.Entity;

namespace ProjectAveryFrontend.Logic.Services.Connections;

public interface IEntityConnectionService
{
    public Task<List<ConsoleMessage>> GetConsoleMessagesAsync(ulong entityId);
    public Task<bool> SubmitConsoleInAsync(string message, ulong entityId);
    public Task<ulong> CreateServerAsync(CreateServerPayload payload);
    public Task<bool> StartEntityAsync(ulong entityId);
}