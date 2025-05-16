using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging;

namespace Infrastructure.FileStorage;

public sealed class AzureBlobStorageService : IFileStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly ILogger<AzureBlobStorageService> _logger;

    public AzureBlobStorageService(
        string connectionString,
        ILogger<AzureBlobStorageService> logger)
    {
        _blobServiceClient = new BlobServiceClient(connectionString);
        _logger = logger;
    }

    public async Task<FileMetadata> UploadAsync(
        Stream fileStream,
        string fileName,
        string containerName = "attachments",
        CancellationToken ct = default)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync(cancellationToken: ct);

        var blobName = $"{Guid.NewGuid()}-{fileName}";
        var blobClient = containerClient.GetBlobClient(blobName);

        await blobClient.UploadAsync(fileStream, true, ct);

        _logger.LogInformation("Uploaded file {FileName} to {BlobUri}",
            fileName, blobClient.Uri);

        return new FileMetadata(
            Id: blobName,
            Uri: blobClient.Uri.ToString(),
            Name: fileName,
            Size: fileStream.Length);
    }

    public async Task<Stream> DownloadAsync(
        string fileId,
        string containerName = "attachments",
        CancellationToken ct = default)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(fileId);

        var stream = new MemoryStream();
        await blobClient.DownloadToAsync(stream, ct);
        stream.Position = 0;

        return stream;
    }
    public async Task UploadLargeFileAsync(
        Stream fileStream, 
        string fileName,
        Action<long> progressCallback = null,
        string containerName = "attachments",
        int bufferSize = 4 * 1024 * 1024)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync();

        var blobName = $"{Guid.NewGuid()}-{fileName}";
        var blobClient = containerClient.GetBlobClient(blobName);

        var progressHandler = progressCallback != null 
            ? new Progress<long>(progressCallback) 
            : null;

        var blobUploadOptions = new BlobUploadOptions
        {
            TransferOptions = new StorageTransferOptions
            {
                InitialTransferSize = bufferSize,
                MaximumTransferSize = bufferSize
            },
            ProgressHandler = progressHandler
        };

        await blobClient.UploadAsync(fileStream, blobUploadOptions);

        _logger.LogInformation("Uploaded large file {FileName} to {BlobUri}", 
            fileName, blobClient.Uri);
    }
}


public record FileMetadata(string Id, string Uri, string Name, long Size);