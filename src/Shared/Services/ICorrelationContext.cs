namespace Shared.Services;

public interface ICorrelationContext
{
    string CorrelationId { get; }
    string? CausationId { get; }

    void SetCorrelationId(string correlationId);
    void SetCausationId(string causationId);
}