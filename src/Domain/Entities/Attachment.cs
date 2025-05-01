using Shared.Base;

namespace Domain.Entities;

public class Attachment : BaseEntity
{
    public string FileName { get; private set; } = string.Empty;
    public string FilePath { get; private set; } = string.Empty;
    public DateTime UploadedAt { get; private set; }

    public Guid TaskItemId { get; private set; }
    public TaskItem TaskItem { get; private set; } = null!;

    private Attachment() { }

    public Attachment(string fileName, string filePath, Guid taskItemId)
    {
        FileName = fileName;
        FilePath = filePath;
        TaskItemId = taskItemId;
        UploadedAt = DateTime.UtcNow;
    }

    public void UpdateFile(string fileName, string filePath)
    {
        FileName = fileName;
        FilePath = filePath;
    }
}