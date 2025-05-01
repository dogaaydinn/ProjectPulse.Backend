namespace Application.Interfaces;

public interface INotificationService
{
    Task NotifyTaskCompletedAsync(Guid taskId, Guid userId);
}