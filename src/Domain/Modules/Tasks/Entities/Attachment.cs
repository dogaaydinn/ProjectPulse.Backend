using Shared.Base;
using Shared.Constants;
using Shared.Validation;

namespace Domain.Modules.Tasks.Entities;

public class Attachment : BaseAuditableEntity
{
    public string FileName { get; private set; } = string.Empty;
    public string FilePath { get; private set; } = string.Empty;
    public string ContentType { get; private set; } = string.Empty;
    public long Size { get; private set; }

    public Guid TaskItemId { get; private set; }
    public TaskItem TaskItem { get; private set; } = null!;

    protected Attachment() { }

    internal Attachment(string fileName, string filePath, string contentType, long size, Guid taskItemId)
    {
        SetFileName(fileName);
        SetFilePath(filePath);
        SetContentType(contentType);
        SetSize(size);
        Guard.AgainstDefaultGuid(taskItemId, ErrorCodes.Validation, ValidationMessages.Attachment.TaskIdRequired);
        TaskItemId = taskItemId;
    }

    public void SetFileName(string name)
    {
        Guard.AgainstEmpty(name, ErrorCodes.Validation, ValidationMessages.Attachment.FileNameRequired);
        FileName = name.Trim();
    }

    public void SetFilePath(string path)
    {
        Guard.AgainstEmpty(path, ErrorCodes.Validation, ValidationMessages.Attachment.FilePathRequired);
        FilePath = path.Trim();
    }

    public void SetContentType(string? type)
    {
        ContentType = string.IsNullOrWhiteSpace(type) ? "application/octet-stream" : type.Trim();
    }

    public void SetSize(long size)
    {
        Guard.AgainstInvalidCondition(size <= 0, ErrorCodes.Validation, ValidationMessages.Attachment.SizeMustBeGreaterThanZero);
        Size = size;
    }

    public void UpdateMetadata(string fileName, string filePath, string? contentType, long size)
    {
        SetFileName(fileName);
        SetFilePath(filePath);
        SetContentType(contentType);
        SetSize(size);
    }
}