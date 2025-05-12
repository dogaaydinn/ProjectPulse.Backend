using Domain.Core.ValueObjects;
using Domain.Modules.Projects.Entities;
using Domain.Modules.Tasks.Entities;
using Domain.Modules.Users.Enums;
using Shared.Base;
using Shared.Validation;
using Shared.Constants;
using Shared.Security;

namespace Domain.Modules.Users.Entities;

public class User : BaseAuditableEntity
{
    public string Username { get; private set; } = string.Empty;
    public Email Email { get; private set; } = null!;
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

    protected User() { }

    public User(string username, Email email, string plainPassword, IUserPasswordHasher hasher)
        : this(username, email, hasher.HashPassword(plainPassword), GlobalRole.User)
    {
    }

    private User(string username, Email email, string passwordHash, GlobalRole globalRole)
    {
        SetUsername(username);
        Email = email;
        SetPasswordHash(passwordHash);
        GlobalRole = globalRole;
    }

    private void SetUsername(string username)
    {
        Guard.AgainstNullOrEmpty(username, ErrorCodes.Validation, ValidationMessages.User.UsernameRequired);
        Username = username.Trim();
    }

    private void SetPasswordHash(string passwordHash)
    {
        Guard.AgainstEmpty(passwordHash, ErrorCodes.User.PasswordHashInvalid, ValidationMessages.User.PasswordHashInvalid);
        PasswordHash = passwordHash;
    }

    public void SetPassword(string plainPassword, IUserPasswordHasher hasher)
    {
        Guard.AgainstNull(hasher, ErrorCodes.User.PasswordHasherNull, ValidationMessages.User.PasswordHasherNull);
        PasswordHash = hasher.HashPassword(plainPassword);
    }

    public bool ValidatePassword(string plainPassword, IUserPasswordHasher hasher)
    {
        Guard.AgainstNull(hasher, ErrorCodes.User.PasswordHasherNull, ValidationMessages.User.PasswordHasherNull);
        return hasher.VerifyPassword(plainPassword, PasswordHash);
    }

    public void ChangeRole(GlobalRole newRole)
    {
        GlobalRole = newRole;
    }
    public void AssignRole(GlobalRole newRole)
    {
        Guard.AgainstNull(newRole, ErrorCodes.Validation, ValidationMessages.User.InvalidRole);
        GlobalRole = newRole;
    }
}