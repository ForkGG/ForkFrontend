using ProjectAveryCommon.Model.Notifications;

namespace ProjectAveryFrontend.Model;

public interface INotificationHandler
{
    public Type Type { get; }

    public Task CallHandlers(AbstractNotification abstractNotification);
}