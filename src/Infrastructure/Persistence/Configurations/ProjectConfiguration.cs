using Domain.Modules.Projects.Entities;
using Domain.Modules.Projects.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Projects");

        builder.HasKey(p => p.Id);

        #region Value Objects: Name & Description

        builder.OwnsOne(p => p.Name, name =>
        {
            name.Property(n => n.Translations)
                .HasColumnName("Name_Translations")
                .HasColumnType("jsonb") // PostgreSQL
                .IsRequired();
        });

        builder.OwnsOne(p => p.Description, desc =>
        {
            desc.Property(d => d.Translations)
                .HasColumnName("Description_Translations")
                .HasColumnType("jsonb"); // nullable
        });

        #endregion

        #region Value Object: Schedule (DateRange)

        builder.OwnsOne(p => p.Schedule, schedule =>
        {
            schedule.Property(s => s.Start)
                .HasColumnName("StartDate")
                .IsRequired();

            schedule.Property(s => s.End)
                .HasColumnName("EndDate");
        });

        #endregion

        #region Enum (as backing fields)

        builder.Property(p => p.Status)
            .HasConversion(
                status => status.Value,
                value => ProjectStatus.FromValue(value))
            .IsRequired();

        builder.Property(p => p.Priority)
            .HasConversion(
                priority => priority.Value,
                value => ProjectPriority.FromValue(value))
            .IsRequired();

        #endregion

        #region Foreign Keys & Navigation

        builder.Property(p => p.ManagerId)
            .IsRequired();

        builder.HasOne(p => p.Manager)
            .WithMany(u => u.ManagedProjects)
            .HasForeignKey(p => p.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion

        #region Auditing Fields (BaseEntity)

        builder.Property(p => p.CreatedByUserId)
            .IsRequired();

      //  builder.Property(p => p.CreatedOnUtc)
           // .IsRequired();

     //   builder.Property(p => p.LastModifiedOnUtc);

        #endregion

        #region Navigation Collections

        builder.Metadata.FindNavigation(nameof(Project.Tasks))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata.FindNavigation(nameof(Project.Milestones))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata.FindNavigation(nameof(Project.Workflows))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata.FindNavigation(nameof(Project.TeamProjects))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata.FindNavigation(nameof(Project.Statuses))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        #endregion
    }
}
