namespace ProjectAveryFrontend.Logic.Services;

public abstract class AbstractConnectionService
{
    protected readonly HttpClient _client;
    protected readonly ILogger<AbstractConnectionService> _logger;

    protected AbstractConnectionService(ILogger<AbstractConnectionService> logger, HttpClient client)
    {
        _logger = logger;
        //TODO CKE Set token in all http requests
        _client = client;
    }
}