using Domain.Primitives.Enums.StructuredEnum;

namespace Domain.Enums;

public sealed class StatusType : StructuredEnum<StatusType>
{
    public static readonly StatusType NotStarted = new(nameof(NotStarted), 1);
    public static readonly StatusType Active = new(nameof(Active), 2);
    public static readonly StatusType Completed = new(nameof(Completed), 3);
    public static readonly StatusType Blocked = new(nameof(Blocked), 4);
    public static readonly StatusType OnHold = new(nameof(OnHold), 5);
    public static readonly StatusType Cancelled = new(nameof(Cancelled), 6);
    public static readonly StatusType Archived = new(nameof(Archived), 7);
    public static readonly StatusType InProgress = new(nameof(InProgress), 8);
    public static readonly StatusType Pending = new(nameof(Pending), 9);
    public static readonly StatusType Resolved = new(nameof(Resolved), 10);
    public static readonly StatusType WorkingOnIt = new(nameof(WorkingOnIt), 11);
    public static readonly StatusType WaitingForFeedback = new(nameof(WaitingForFeedback), 12);
    public static readonly StatusType Stuck = new(nameof(Stuck), 13);
    public static readonly StatusType WaitingForApproval = new(nameof(WaitingForApproval), 14);
    public static readonly StatusType InReview = new(nameof(InReview), 15);
    public static readonly StatusType WaitingForToAssign = new(nameof(WaitingForToAssign), 16);
    public static readonly StatusType WaitingForTest = new(nameof(WaitingForTest), 17);
    public static readonly StatusType WaitingForRelease = new(nameof(WaitingForRelease), 18);
    public static readonly StatusType WaitingForDeployment = new(nameof(WaitingForDeployment), 19);
    public static readonly StatusType Designing = new(nameof(Designing), 20);
    public static readonly StatusType Testing = new(nameof(Testing), 21);
    public static readonly StatusType ReadyForTesting = new(nameof(ReadyForTesting), 22);
    public static readonly StatusType ReadyToBeRelease = new(nameof(ReadyToBeRelease), 23);
    public static readonly StatusType ReadyToGoLive = new(nameof(ReadyToGoLive), 24);
    
    
    private StatusType(string name, int value) : base(name, value) { }
}