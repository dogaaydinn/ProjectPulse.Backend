using Domain.Primitives.Enums.StructuredEnum;

namespace Domain.Enums;

public sealed class TaskRole : StructuredEnum<TaskRole>
{
    public static readonly TaskRole Assignee = new(nameof(Assignee), 1);
    public static readonly TaskRole Reviewer = new(nameof(Reviewer), 2);
    public static readonly TaskRole Watcher = new(nameof(Watcher), 3);
    public static readonly TaskRole Stakeholder = new(nameof(Stakeholder), 4);
    public static readonly TaskRole Collaborator = new(nameof(Collaborator), 5);
    public static readonly TaskRole ProjectManager = new(nameof(ProjectManager), 6);    
    public static readonly TaskRole ProjectOwner = new(nameof(ProjectOwner), 7);
    public static readonly TaskRole Developer = new(nameof(Developer), 8);
    public static readonly TaskRole Tester = new(nameof(Tester), 9);
    public static readonly TaskRole Designer = new(nameof(Designer), 10);
    public static readonly TaskRole Architect = new(nameof(Architect), 11);
    public static readonly TaskRole BusinessAnalyst = new(nameof(BusinessAnalyst), 12);
    public static readonly TaskRole DevOps = new(nameof(DevOps), 13);
    public static readonly TaskRole ScrumMaster = new(nameof(ScrumMaster), 14);
    public static readonly TaskRole ProductOwner = new(nameof(ProductOwner), 15);
    public static readonly TaskRole QualityAssurance = new(nameof(QualityAssurance), 16);
    public static readonly TaskRole SystemAdministrator = new(nameof(SystemAdministrator), 17);
    public static readonly TaskRole DatabaseAdministrator = new(nameof(DatabaseAdministrator), 18);
    public static readonly TaskRole SecurityAnalyst = new(nameof(SecurityAnalyst), 19);
    public static readonly TaskRole NetworkAdministrator = new(nameof(NetworkAdministrator), 20);
    public static readonly TaskRole TechnicalWriter = new(nameof(TechnicalWriter), 21);
    public static readonly TaskRole SupportEngineer = new(nameof(SupportEngineer), 22);
    public static readonly TaskRole DataAnalyst = new(nameof(DataAnalyst), 23);
    public static readonly TaskRole DataScientist = new(nameof(DataScientist), 24);
    public static readonly TaskRole SystemAnalyst = new(nameof(SystemAnalyst), 25);
    public static readonly TaskRole ReleaseManager = new(nameof(ReleaseManager), 26);
    public static readonly TaskRole ConfigurationManager = new(nameof(ConfigurationManager), 27);
    public static readonly TaskRole ChangeManager = new(nameof(ChangeManager), 28);
    public static readonly TaskRole IncidentManager = new(nameof(IncidentManager), 29);
    

    private TaskRole(string name, int value) : base(name, value) { }
}