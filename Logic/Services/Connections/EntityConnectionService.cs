using ProjectAveryFrontend.Logic.Services.HttpsClients;

namespace ProjectAveryFrontend.Logic.Services.Connections;

public class EntityConnectionService : AbstractConnectionService, IEntityConnectionService
{
    public EntityConnectionService(ILogger<EntityConnectionService> logger, BackendClient client) : base(logger, client)
    {
    }

    public async Task<bool> SubmitConsoleInAsync(string message, ulong entityId)
    {
        try
        {
            var response = await _client.PostAsync($"/v1/entity/{entityId}/consolein", new StringContent(message));
            return response.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            //TODO properly display errors
            _logger.LogError(e, "Error while sending Console Input");
            return false;
        }
    }
}