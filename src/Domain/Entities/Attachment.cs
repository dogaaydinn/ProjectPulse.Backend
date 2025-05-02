using Shared.Base;
using Shared.Exceptions;

namespace Domain.Entities;

public class Attachment : BaseEntity
{
    public string FileName { get; private set; } = string.Empty;
    public string FilePath { get; private set; } = string.Empty;
    public string ContentType { get; private set; } = string.Empty;
    public long Size { get; private set; }
    public DateTime UploadedAt { get; private set; }

    public Guid TaskItemId { get; private set; }
    public TaskItem TaskItem { get; private set; } = null!;

    protected Attachment() { }

    internal Attachment(string fileName, string filePath, string contentType, long size, Guid taskItemId)
    {
        SetFileName(fileName);
        SetFilePath(filePath);
        SetContentType(contentType);
        SetSize(size);
        TaskItemId = taskItemId;
        UploadedAt = DateTime.UtcNow;
    }

    private void SetFileName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new AppException("Validation.Attachment.FileName", "File name is required.");
        FileName = name.Trim();
    }

    private void SetFilePath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new AppException("Validation.Attachment.FilePath", "File path is required.");
        FilePath = path;
    }

    private void SetContentType(string type)
    {
        ContentType = type ?? "application/octet-stream";
    }

    private void SetSize(long size)
    {
        if (size <= 0)
            throw new AppException("Validation.Attachment.Size", "File size must be greater than zero.");
        Size = size;
    }
}