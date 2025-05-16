using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Correlation;

public class CorrelationIdProvider : ICorrelationIdProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CorrelationIdProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetCorrelationId()
        => _httpContextAccessor.HttpContext?.TraceIdentifier;
}