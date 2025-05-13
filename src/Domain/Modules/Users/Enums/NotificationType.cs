using Domain.Core.Primitives.Enums.Base;

namespace Domain.Modules.Users.Enums;

public sealed class NotificationType : StructuredEnum<NotificationType>
{
    public static readonly NotificationType Task = new(nameof(Task), 1);
    public static readonly NotificationType Project = new(nameof(Project), 2);
    public static readonly NotificationType Comment = new(nameof(Comment), 3);
    public static readonly NotificationType System = new(nameof(System), 4);

    private NotificationType(string name, int value) : base(name, value) { }
}