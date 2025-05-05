using Domain.Primitives.Enums.StructuredEnum;

namespace Domain.Enums;

public sealed class TaskPriority : StructuredEnum<TaskPriority>
{
    public static readonly TaskPriority Low = new(nameof(Low), 1);
    public static readonly TaskPriority Medium = new(nameof(Medium), 2);
    public static readonly TaskPriority High = new(nameof(High), 3);
    public static readonly TaskPriority Urgent = new(nameof(Urgent), 4);
    public static readonly TaskPriority Critical = new(nameof(Critical), 5);
    public static readonly TaskPriority Unspecified = new(nameof(Unspecified), 0);
    public static readonly TaskPriority Blocker = new(nameof(Blocker), 6);
    public static readonly TaskPriority Major = new(nameof(Major), 7);
    public static readonly TaskPriority Minor = new(nameof(Minor), 8);
    public static readonly TaskPriority Trivial = new(nameof(Trivial), 9);
    public static readonly TaskPriority UserInitiated = new(nameof(UserInitiated), 10);
    public static readonly TaskPriority NotUrgent = new(nameof(NotUrgent), 11);
    

    private TaskPriority(string name, int value) : base(name, value) { }
}