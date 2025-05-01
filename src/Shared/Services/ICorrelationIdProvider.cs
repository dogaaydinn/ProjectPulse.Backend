namespace Shared.Services;

public interface ICorrelationIdProvider
{
    string? GetCorrelationId();
}