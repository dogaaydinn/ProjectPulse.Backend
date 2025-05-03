using Domain.Enums;
using Domain.ValueObjects;
using Shared.Base;
using Shared.Exceptions;

namespace Domain.Entities;

public class User : BaseAuditableEntity
{
    public string Username { get; private set; } = string.Empty;
    public Email Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = string.Empty;
    public GlobalRole GlobalRole { get; private set; } = GlobalRole.User;
    
    public ICollection<Project> ManagedProjects { get; private set; } = new List<Project>();
    public ICollection<TaskItem> AssignedTasks { get; private set; } = new List<TaskItem>();
    public ICollection<TaskItem> ReportedTasks { get; private set; } = new List<TaskItem>();
    public ICollection<Comment> Comments { get; private set; } = new List<Comment>();
    public ICollection<Notification> Notifications { get; private set; } = new List<Notification>();
    public ICollection<TimeLog> TimeLogs { get; private set; } = new List<TimeLog>();
    public ICollection<TaskAssignment> TaskAssignments { get; private set; } = new List<TaskAssignment>();
    public ICollection<UserTeam> UserTeams { get; private set; } = new List<UserTeam>();

    protected User() { }

    public User(string username, Email email, string passwordHash, GlobalRole role = GlobalRole.User)
    {
        SetUsername(username);
        SetPassword(passwordHash);
        Email = email;
        GlobalRole = role;
    }

    private void SetUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new AppException("Validation.User.Username", "Username is required.");
        Username = username.Trim();
    }

    private void SetPassword(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new AppException("Validation.User.Password", "Password cannot be empty.");
        PasswordHash = passwordHash;
    }

    public void ChangeRole(GlobalRole newRole)
    {
        GlobalRole = newRole;
    }
}