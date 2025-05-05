using Domain.Primitives.Enums.StructuredEnum;

namespace Domain.Enums;

public sealed class ProjectPriority : StructuredEnum<ProjectPriority>
{
    public static readonly ProjectPriority Low = new(nameof(Low), 1);
    public static readonly ProjectPriority Medium = new(nameof(Medium), 2);
    public static readonly ProjectPriority High = new(nameof(High), 3);
    public static readonly ProjectPriority Critical = new(nameof(Critical), 4);
    public static readonly ProjectPriority Unspecified = new(nameof(Unspecified), 0);
    public static readonly ProjectPriority Blocker = new(nameof(Blocker), 5);
    public static readonly ProjectPriority Major = new(nameof(Major), 6);
    public static readonly ProjectPriority Minor = new(nameof(Minor), 7);
    public static readonly ProjectPriority Trivial = new(nameof(Trivial), 8);
    public static readonly ProjectPriority UserInitiated = new(nameof(UserInitiated), 9);
    public static readonly ProjectPriority NotUrgent = new(nameof(NotUrgent), 10);
    public static readonly ProjectPriority Urgent = new(nameof(Urgent), 11);
    

    private ProjectPriority(string name, int value) : base(name, value) { }
}
