using Shared.Results;

namespace Shared.Abstractions.Logging;

public interface IErrorLogger
{
    void LogError(Error error);
}