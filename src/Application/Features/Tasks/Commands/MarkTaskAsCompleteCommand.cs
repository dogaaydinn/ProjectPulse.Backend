using ProjectPulse.Shared.Results;
using Shared.Results;

public record MarkTaskAsCompleteCommand(Guid TaskId) : IRequest<Result>;