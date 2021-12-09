using ProjectAveryCommon.Model.Notifications;

namespace ProjectAveryFrontend.Logic.Services.Notifications;

public interface INotificationService
{
    void Register<T>(Func<AbstractNotification, Task> handler) where T : AbstractNotification;
    Task StartupAsync();
}