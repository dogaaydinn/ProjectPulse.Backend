namespace Shared.Services;

public interface IFileStorageService
{
    Task<FileMetadata> UploadAsync(
        Stream  fileStream,
        string  fileName,
        CancellationToken cancellationToken = default);

    Task<Stream> DownloadAsync(
        string  fileId,
        CancellationToken cancellationToken = default);

    Task<FileMetadata> UploadLargeFileAsync(
        Stream            fileStream,
        string            fileName,
        IProgress<long>?  progress           = null,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        string  fileId,
        CancellationToken cancellationToken = default);
}