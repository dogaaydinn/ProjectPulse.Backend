// Infrastructure/Services/Internal/IOutboxProcessor.cs
namespace Infrastructure.Services.Internal;

using Shared.Results;

public interface IOutboxProcessor
{
    Task<Result> ProcessPendingMessagesAsync();
    Task<Result> ScheduleRetryAsync(Guid messageId);
}