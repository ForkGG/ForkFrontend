using ForkCommon.Model.Notifications;

namespace ForkFrontend.Logic.Services.Notifications;

public interface INotificationService
{
    void Register<T>(Func<T, Task> handler) where T : AbstractNotification;

    void Unregister<T>(object caller) where T : AbstractNotification;
    Task StartupAsync();
}