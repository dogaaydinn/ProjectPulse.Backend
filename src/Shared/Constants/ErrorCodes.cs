namespace Shared.Constants;

public static class ErrorCodes
{
    //  Global System-Level Errors
    public static class General
    {
        public const string Unexpected = "System.Unexpected";
        public const string Conflict = "System.Conflict";
    }

    //  Global Validation Errors
    public static class Validation
    {
        public const string Required = "Validation.Required";
        public const string InvalidFormat = "Validation.InvalidFormat";
        public const string Default = "Validation.Default";
        public const string EmptyResult = "Validation.EmptyResult";
    }

    //  Feature-Specific Errors: User
    public static class User
    {
        public const string PasswordHashInvalid = "User.PasswordHashInvalid";
        public const string PasswordHasherNull = "User.PasswordHasherNull";
        public const string NotFound = "User.NotFound";
        public const string AlreadyExists = "User.AlreadyExists";
    }

    //  Feature-Specific Errors: Project
    public static class Project
    {
        public const string NotFound = "Project.NotFound";
        public const string InvalidDateRange = "Project.InvalidDateRange";
        public const string NameAlreadyExists = "Project.NameAlreadyExists";

    }

    public static class Security
    {
        public const string Unauthorized = "Security.Unauthorized";
        public const string Forbidden = "Security.Forbidden";
    }

    //  Feature-Specific Errors: Schedule
    public static class Schedule
    {
        public const string InvalidSlot = "Schedule.InvalidSlot";
        public const string Conflict = "Schedule.Conflict";
    }
    public static class LocalizedString
    {
        public const string Required = "LocalizedString.Required";
        public const string NoValidTranslations = "LocalizedString.NoValidTranslations";
        public const string InvalidCulture = "LocalizedString.InvalidCulture";
        public const string MissingCulture = "LocalizedString.MissingCulture";
        
    }
}