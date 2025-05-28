namespace Shared.Services;

public sealed record FileMetadata(
    string Id,
    string Uri,
    string Name,
    long   Size
);