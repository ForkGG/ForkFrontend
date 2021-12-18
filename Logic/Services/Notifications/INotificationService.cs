using ProjectAveryCommon.Model.Notifications;

namespace ProjectAveryFrontend.Logic.Services.Notifications;

public interface INotificationService
{
    void Register<T>(Func<AbstractNotification, Task> handler) where T : AbstractNotification;

    void Unregister<T>(object caller) where T : AbstractNotification;
    Task StartupAsync();
}