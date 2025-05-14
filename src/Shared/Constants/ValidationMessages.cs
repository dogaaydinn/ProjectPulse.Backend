namespace Shared.Constants;

public static class ValidationMessages
{
    public static class Project
    {
        public const string ProjectNameRequired = "Project name is required.";
        public const string ManagerIdRequired = "Manager ID is required.";
        public const string ProjectIdRequired = "Project ID is required.";
        public const string ProjectDescriptionRequired = "Project description is required.";
        public const string ScheduleRequired = "Project schedule is required.";
    }
    
    public static class Task
    {
        public const string TitleRequired = "Task title is required.";
    }
    
    public static class Team
    {
        public const string TeamNameRequired = "Team name is required.";
    }
    
    public static class TeamProject
    {
        public const string TeamIdRequired = "Team ID is required.";
    }

    public static class Common
    {
        public const string StartDateMustBeBeforeEndDate = "Start date must be before end date.";
        public const string ProjectIdRequired = "Projects ID is required.";
        public const string TaskIdRequired = "Task ID is required.";
        public const string LocalizedStringRequired = "At least one localized value must be provided.";
        public const string ScheduleRequired = "Schedule is required.";
        public const string NameRequired = "Name is required.";
    }

    public static class User
    {
        public const string UsernameRequired = "Username is required.";
        public const string PasswordHashInvalid = "Password hash is invalid.";
        public const string PasswordHasherNull = "Password hasher cannot be null.";
        public const string InvalidRole = "Invalid role.";
        public const string DisplayNameRequired = "Display name must not be empty.";
        public const string EmailRequired = "Email address is required.";
    }
    
    public static class Status
    {
        public const string NameRequired = "Status name is required.";
        public const string OrderMustBeNonNegative = "Status order cannot be negative.";
    }
    
    public static class Label
    {
        public const string NameRequired = "Label name is required.";
    }
    
    public static class Comment
    {
        public const string ContentRequired = "Comment content is required.";
        public const string TaskRequired = "Comment must be associated with a task.";
        public const string AuthorRequired = "Comment must have an author.";
        public const string AuthorIdRequired = "Author ID is required.";
        public const string CommentContentRequired = "Comment content is required.";
    }
    
    public static class Enum
    {
        public const string InvalidName = "Invalid enum name: {0}.";
    }
    public static class Attachment
    {
        public const string FileNameRequired = "File name is required.";
        public const string FilePathRequired = "File path is required.";
        public const string SizeMustBeGreaterThanZero = "File size must be greater than zero.";
        public const string TaskIdRequired = "Attachment must be associated with a task.";
    }
    public static class TimeLog
    {
        public const string TaskIdRequired = "Task ID is required.";
        public const string UserIdRequired = "User ID is required.";
        public const string StartTimeMustBeBeforeEndTime = "Start time must be before end time.";
    }
    
    public static class Milestone
    {
        public const string NameRequired = "Milestone name is required.";
        public const string AlreadyCompleted = "Milestone is already completed.";
    }


}