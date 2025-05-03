namespace Shared.Constants;

public static class ValidationMessages
{
    public static class Project
    {
        public const string ProjectNameRequired = "Project name is required.";
        public const string StartDateRequired = "Start date is required.";
        public const string EndDateRequired = "End date is required.";
        public const string ManagerIdRequired = "Manager ID is required.";
    }
    
    public static class Task
    {
        public const string TitleRequired = "Task title is required.";
        public const string TaskTitleRequired = "Task title is required.";
        public const string DescriptionRequired = "Task description is required.";
        public const string AssigneeIdRequired = "Assignee ID is required.";
        public const string DueDateRequired = "Due date is required.";
    }

    public static class Comment
    {

        public const string CommentContentRequired = "Comment content is required.";
        public const string AuthorIdRequired = "Author ID is required.";
        public const string TaskIdRequired = "Task ID is required.";
        
    }
    
    public static class Team
    {
        public const string NameRequired = "Team name is required.";
        public const string DescriptionRequired = "Team description is required.";
        public const string ManagerIdRequired = "Manager ID is required.";
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
    }
}