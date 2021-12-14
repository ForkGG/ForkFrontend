using System.Net.Http.Headers;
using ProjectAveryFrontend.Logic.Services.HttpsClients;

namespace ProjectAveryFrontend.Logic.Services.Connections;

public abstract class AbstractConnectionService
{
    protected readonly HttpClient _client;
    protected readonly ILogger<AbstractConnectionService> _logger;

    protected AbstractConnectionService(ILogger<AbstractConnectionService> logger, BackendClient client)
    {
        _logger = logger;
        //TODO CKE Set token in all http requests
        _client = client.Client;
        _client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("asdf");
    }
}