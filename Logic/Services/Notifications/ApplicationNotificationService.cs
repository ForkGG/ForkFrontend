using System.Net.WebSockets;
using System.Text;
using ProjectAveryCommon.ExtensionMethods;
using ProjectAveryCommon.Model.Notifications;

namespace ProjectAveryFrontend.Logic.Services.Notifications;

public class ApplicationNotificationService : INotificationService
{
    private readonly CancellationTokenSource _cancellationTokenSource;
    private readonly ILogger<ApplicationNotificationService> _logger;
    private readonly ClientWebSocket _webSocket;

    public ApplicationNotificationService(ILogger<ApplicationNotificationService> logger)
    {
        logger.LogInformation("Initializing NotificationService");
        _logger = logger;
        _cancellationTokenSource = new CancellationTokenSource();
        _webSocket = new ClientWebSocket();
        RegisteredHandlers = new Dictionary<Type, List<Func<AbstractNotification, Task>>>();
    }

    private Dictionary<Type, List<Func<AbstractNotification, Task>>> RegisteredHandlers { get; }

    public void Register<T>(Func<AbstractNotification, Task> handler) where T : AbstractNotification
    {
        _logger.LogDebug($"Registering new NotificationHandler `{handler.Method.Name}` for {nameof(T)}");
        if (!RegisteredHandlers.ContainsKey(typeof(T)))
        {
            var newHandlers = new List<Func<AbstractNotification, Task>>();
            RegisteredHandlers[typeof(T)] = newHandlers;
        }

        RegisteredHandlers[typeof(T)].Add(handler);
    }

    public async Task HandleAsync(AbstractNotification? notification)
    {
        if (notification == null) _logger.LogDebug("Can't handle notification because it was null");
        var handlers = RegisteredHandlers.ContainsKey(notification.GetType())
            ? RegisteredHandlers[notification.GetType()]
            : new List<Func<AbstractNotification, Task>>();
        _logger.LogDebug($"Handling {nameof(notification)} with {handlers.Count} handlers");
        foreach (Func<AbstractNotification, Task> handler in handlers) await handler.Invoke(notification);
    }

    public async Task StartupAsync()
    {
        await _webSocket.ConnectAsync(new Uri("ws://localhost:35566"), _cancellationTokenSource.Token);
        _ = ReceiveLoop();
    }

    private async Task ReceiveLoop()
    {
        //https://gist.github.com/SteveSandersonMS/5aaff6b010b0785075b0a08cc1e40e01
        var buffer = new ArraySegment<byte>(new byte[1024]);

        while (!_cancellationTokenSource.IsCancellationRequested && _webSocket.State != WebSocketState.Closed)
            try
            {
                var received = await _webSocket.ReceiveAsync(buffer, _cancellationTokenSource.Token);
                var receivedAsText = Encoding.UTF8.GetString(buffer.Array, 0, received.Count);
                while (!received.EndOfMessage)
                {
                    received = await _webSocket.ReceiveAsync(buffer, _cancellationTokenSource.Token);
                    receivedAsText += Encoding.UTF8.GetString(buffer.Array, 0, received.Count);
                }

                try
                {
                    AbstractNotification notification = receivedAsText.FromJson<AbstractNotification>();
                    _logger.LogDebug($"Handling notification: {receivedAsText}");
                    await HandleAsync(notification);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"Exception while parsing and handling notification: {receivedAsText}");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception while receiving notification");
            }
    }
}