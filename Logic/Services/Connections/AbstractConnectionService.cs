using System.Net.Http.Headers;
using System.Text;
using ProjectAveryCommon.ExtensionMethods;
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

    protected async Task<T?> GetFromJsonAsync<T>(string url)
    {
        var response = await _client.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();
        return content.FromJson<T>();
    }

    protected async Task<HttpResponseMessage> PostAsJsonAsync(string url, object payload)
    {
        return await _client.PostAsync("/v1/entity/createserver",
            new StringContent(payload.ToJson(), Encoding.UTF8, "application/json"));
    }

    protected async Task<T?> PostAsJsonAsync<T>(string url, object payload)
    {
        var response = await _client.PostAsync("/v1/entity/createserver",
            new StringContent(payload.ToJson(), Encoding.UTF8, "application/json"));
        var content = await response.Content.ReadAsStringAsync();
        return content.FromJson<T>();
    }
}