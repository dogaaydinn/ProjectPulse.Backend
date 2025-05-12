using Domain.Core.Primitives.Enums.Base;

namespace Domain.Modules.Projects.Enums;

public sealed class ProjectStatus : StructuredEnum<ProjectStatus>
{
    public static readonly ProjectStatus Planned = new(nameof(Planned), 1);
    public static readonly ProjectStatus InProgress = new(nameof(InProgress), 2);
    public static readonly ProjectStatus Completed = new(nameof(Completed), 3);
    public static readonly ProjectStatus OnHold = new(nameof(OnHold), 4);
    public static readonly ProjectStatus Cancelled = new(nameof(Cancelled), 5);
    public static readonly ProjectStatus OnTrack = new(nameof(OnTrack), 6);
    public static readonly ProjectStatus AtRisk = new(nameof(AtRisk), 7);
    public static readonly ProjectStatus Delayed = new(nameof(Delayed), 8);
    public static readonly ProjectStatus NotStarted = new(nameof(NotStarted), 9);
    public static readonly ProjectStatus CompletedWithIssues = new(nameof(CompletedWithIssues), 10);
    public static readonly ProjectStatus Abandoned = new(nameof(Abandoned), 11);
    public static readonly ProjectStatus BehindSchedule = new(nameof(BehindSchedule), 12);
    public static readonly ProjectStatus AheadOfSchedule = new(nameof(AheadOfSchedule), 13);
    public static readonly ProjectStatus UnderReview = new(nameof(UnderReview), 14);
    public static readonly ProjectStatus AwaitingApproval = new(nameof(AwaitingApproval), 15);
    public static readonly ProjectStatus InLimbo = new(nameof(InLimbo), 16);
    public static readonly ProjectStatus OverBudget = new(nameof(OverBudget), 17);
    public static readonly ProjectStatus UnderBudget = new(nameof(UnderBudget), 18);
    public static readonly ProjectStatus InDevelopment = new(nameof(InDevelopment), 19);
    public static readonly ProjectStatus InTesting = new(nameof(InTesting), 20);
    public static readonly ProjectStatus InDeployment = new(nameof(InDeployment), 21);
    public static readonly ProjectStatus InMaintenance = new(nameof(InMaintenance), 22);
    public static readonly ProjectStatus InTransition = new(nameof(InTransition), 23);

    private ProjectStatus(string name, int value) : base(name, value) { }
}