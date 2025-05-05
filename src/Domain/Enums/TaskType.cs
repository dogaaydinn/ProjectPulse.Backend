using Domain.Primitives.Enums.StructuredEnum;

namespace Domain.Enums;

public sealed class TaskType : StructuredEnum<TaskType>
{
    public static readonly TaskType Feature = new(nameof(Feature), 1);
    public static readonly TaskType Bug = new(nameof(Bug), 2);
    public static readonly TaskType Improvement = new(nameof(Improvement), 3);
    public static readonly TaskType Research = new(nameof(Research), 4);
    public static readonly TaskType Task = new(nameof(Task), 5);
    public static readonly TaskType SubTask = new(nameof(SubTask), 6);
    public static readonly TaskType Epic = new(nameof(Epic), 7);
    public static readonly TaskType Story = new(nameof(Story), 8);
    public static readonly TaskType ChangeRequest = new(nameof(ChangeRequest), 9);
    public static readonly TaskType Incident = new(nameof(Incident), 10);
    public static readonly TaskType Spike = new(nameof(Spike), 11);
    public static readonly TaskType NewFeature = new(nameof(NewFeature), 12);
    public static readonly TaskType TechnicalDebt = new(nameof(TechnicalDebt), 13);
    public static readonly TaskType Refactoring = new(nameof(Refactoring), 14);
    public static readonly TaskType Maintenance = new(nameof(Maintenance), 15);
    public static readonly TaskType Documentation = new(nameof(Documentation), 16);
    public static readonly TaskType Review = new(nameof(Review), 17);
    public static readonly TaskType Testing = new(nameof(Testing), 18);
    public static readonly TaskType Deployment = new(nameof(Deployment), 19);
    public static readonly TaskType Configuration = new(nameof(Configuration), 20);
    public static readonly TaskType Monitoring = new(nameof(Monitoring), 21);
    public static readonly TaskType Support = new(nameof(Support), 22);
    public static readonly TaskType Training = new(nameof(Training), 23);
    public static readonly TaskType Planning = new(nameof(Planning), 24);
    public static readonly TaskType Design = new(nameof(Design), 25);
    public static readonly TaskType Analysis = new(nameof(Analysis), 26);
    public static readonly TaskType ReviewMeeting = new(nameof(ReviewMeeting), 27);

    private TaskType(string name, int value) : base(name, value) { }
}