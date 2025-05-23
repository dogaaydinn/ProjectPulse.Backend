using Shared.Abstractions.Localization;

namespace Shared.Results;

public static class ErrorConfiguration
{
    public static IErrorLocalizer? FallbackLocalizer { get; private set; }
    public static void Configure(IErrorLocalizer localizer)
        => FallbackLocalizer = localizer;
}