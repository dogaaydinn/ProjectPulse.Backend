using Domain.Enums;
using Shared.Base;

namespace Domain.Entities;

public class User : BaseAuditableEntity
{
    public string Username { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public GlobalRole GlobalRole { get; private set; } = GlobalRole.User;

    // Navigation Properties

    public ICollection<Project> ManagedProjects { get; private set; } = new List<Project>();
    public ICollection<TaskItem> AssignedTasks { get; private set; } = new List<TaskItem>();
    public ICollection<TaskItem> ReportedTasks { get; private set; } = new List<TaskItem>();
    public ICollection<Comment> Comments { get; private set; } = new List<Comment>();
    public ICollection<Notification> Notifications { get; private set; } = new List<Notification>();
    public ICollection<TimeLog> TimeLogs { get; private set; } = new List<TimeLog>();
    public ICollection<TaskAssignment> TaskAssignments { get; private set; } = new List<TaskAssignment>();
    public ICollection<UserTeam> UserTeams { get; private set; } = new List<UserTeam>();

    private User()
    {
    }

    public User(string username, string email, string passwordHash, string globalRole = "User")
    {
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
        GlobalRole = Enum.TryParse<GlobalRole>(globalRole, true, out var role) ? role : GlobalRole.User;
    }
}