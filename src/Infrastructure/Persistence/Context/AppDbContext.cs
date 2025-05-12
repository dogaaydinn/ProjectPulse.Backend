using Domain.Modules.Projects.Entities;
using Domain.Modules.Tasks.Entities;
using Domain.Modules.Teams.Entities;
using Domain.Modules.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<TaskItem> Tasks => Set<TaskItem>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Workflow> Workflows => Set<Workflow>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Milestone> Milestones => Set<Milestone>();
    public DbSet<Attachment> Attachments => Set<Attachment>();
    public DbSet<TaskDependency> TaskDependencies => Set<TaskDependency>();
    public DbSet<TaskAssignment> TaskAssignments => Set<TaskAssignment>();
    public DbSet<TimeLog> TimeLogs => Set<TimeLog>();
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<UserTeam> UserTeams => Set<UserTeam>();
    public DbSet<TaskLabel> TaskLabels => Set<TaskLabel>();
    public DbSet<Label> Labels => Set<Label>();
    public DbSet<Status> Statuses => Set<Status>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<TeamProject> TeamProjects => Set<TeamProject>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}