using System.Diagnostics;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using ProjectAveryCommon.ExtensionMethods;
using ProjectAveryCommon.Model.Notifications;
using ProjectAveryFrontend.Logic.Services.Managers;
using ProjectAveryFrontend.Model;

namespace ProjectAveryFrontend.Logic.Services.Notifications;

public class ApplicationNotificationService : INotificationService
{
    private const int BUFFER_SIZE = 2048;

    private readonly IApplicationStateManager _applicationState;
    private readonly CancellationTokenSource _cancellationTokenSource;
    private readonly ILogger<ApplicationNotificationService> _logger;
    private readonly Uri _webSocketUri = new("ws://localhost:35566");
    private ClientWebSocket? _webSocket;

    public ApplicationNotificationService(ILogger<ApplicationNotificationService> logger,
        IApplicationStateManager applicationState)
    {
        logger.LogInformation("Initializing NotificationService");
        _logger = logger;
        _applicationState = applicationState;
        _cancellationTokenSource = new CancellationTokenSource();
        RegisteredHandlers = new Dictionary<Type, List<Func<AbstractNotification, Task>>>();
    }

    private Dictionary<Type, List<Func<AbstractNotification, Task>>> RegisteredHandlers { get; }

    public void Register<T>(Func<AbstractNotification, Task> handler) where T : AbstractNotification
    {
        _logger.LogDebug($"Registering new NotificationHandler `{handler.Method.Name}` for {typeof(T)}");
        if (!RegisteredHandlers.ContainsKey(typeof(T)))
        {
            var newHandlers = new List<Func<AbstractNotification, Task>>();
            RegisteredHandlers[typeof(T)] = newHandlers;
        }

        RegisteredHandlers[typeof(T)].Add(handler);
    }

    public void Unregister<T>(object caller) where T : AbstractNotification
    {
        if (RegisteredHandlers.ContainsKey(typeof(T)))
        {
            _logger.LogDebug(
                $"Unregistering {RegisteredHandlers[typeof(T)].Count(e => e.Target == caller)} NotificationHandlers for {typeof(T)}");
            RegisteredHandlers[typeof(T)].RemoveAll(e => e.Target == caller);
        }
    }

    public async Task StartupAsync()
    {
        while (!_cancellationTokenSource.IsCancellationRequested)
        {
            _webSocket = new ClientWebSocket();
            try
            {
                IAsyncEnumerable<string> messages = ConnectAsync(_cancellationTokenSource.Token);
                await foreach (string message in messages) await HandleMessage(message);
                _applicationState.WebsocketStatus = WebsocketStatus.Disconnected;
                _logger.LogDebug("Websocket closed. Reconnecting in 500ms");
                _webSocket.Abort();
                _webSocket.Dispose();
                await Task.Delay(500);
            }
            catch (Exception e)
            {
                _logger.LogDebug($"Error in Websocket: {e.Message}");
                _webSocket.Abort();
                _webSocket.Dispose();
                await Task.Delay(500);
            }
        }
    }

    private async Task HandleMessage(string message)
    {
        AbstractNotification notification = message.FromJson<AbstractNotification>();
        if (notification == null)
        {
            _logger.LogDebug("Can't handle notification because it was null");
            return;
        }

        var handlers = RegisteredHandlers.ContainsKey(notification.GetType())
            ? RegisteredHandlers[notification.GetType()]
            : new List<Func<AbstractNotification, Task>>();
        _logger.LogDebug($"Handling {notification.GetType()} with {handlers.Count} handlers\n{notification.ToJson()}");
        foreach (Func<AbstractNotification, Task> handler in handlers)
            await handler.Invoke(notification);
    }

    /// <summary>
    ///     Connect to the websocket and begin yielding messages
    ///     received from the connection.
    /// </summary>
    private async IAsyncEnumerable<string> ConnectAsync(
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        if (_webSocket == null) yield break;
        await _webSocket.ConnectAsync(_webSocketUri, cancellationToken);
        _applicationState.WebsocketStatus = WebsocketStatus.Connected;
        // TODO CKE actual token
        await SendMessageAsync("dummyToken", cancellationToken);
        var buffer = new ArraySegment<byte>(new byte[BUFFER_SIZE]);
        while (!cancellationToken.IsCancellationRequested)
        {
            WebSocketReceiveResult result;
            await using var ms = new MemoryStream();
            do
            {
                result = await _webSocket.ReceiveAsync(buffer, cancellationToken);
                Debug.Assert(buffer.Array != null, "buffer.Array != null");
                ms.Write(buffer.Array, buffer.Offset, result.Count);
            } while (!result.EndOfMessage);

            ms.Seek(0, SeekOrigin.Begin);

            yield return Encoding.UTF8.GetString(ms.ToArray());

            if (result.MessageType == WebSocketMessageType.Close)
                break;
        }
    }

    private async Task SendMessageAsync(string message, CancellationToken cancellationToken)
    {
        if (_applicationState.WebsocketStatus != WebsocketStatus.Connected)
        {
            _logger.LogError($"Failed to write WebSocket message. WebSocket is not connected!\n{message}");
            return;
        }

        if (_webSocket == null || _webSocket.State != WebSocketState.Open)
        {
            _logger.LogError(
                $"Failed to write WebSocket message. WebSocket connection is either not existent or closed!\n{message}");
            return;
        }

        var messageInBytes = Encoding.UTF8.GetBytes(message);
        _logger.LogDebug($"Sending message with {messageInBytes.Length} Bytes");
        for (int i = 0; i < messageInBytes.Length; i += BUFFER_SIZE)
        {
            var chunk = new ArraySegment<byte>(messageInBytes.Skip(i).Take(BUFFER_SIZE).ToArray());
            await _webSocket.SendAsync(chunk, WebSocketMessageType.Text, i + BUFFER_SIZE >= messageInBytes.Length,
                cancellationToken);
        }
    }
}