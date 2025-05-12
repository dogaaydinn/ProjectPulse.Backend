namespace Shared.Constants;

public static class ErrorCodes
{
    public const string NotFound = "Error.NotFound";
    public const string Validation = "Error.Validation";
    public const string Unexpected = "Error.Unexpected";
    public const string Schedule = "Error.Schedule";
    
    public static class Email
    {
        public const string Invalid = "Error.Email.Invalid";
        public const string Empty = "Error.Email.Empty";
    }
    
    public static class User
    {
        public const string UsernameRequired = "Validation.User.UsernameRequired";
        public const string PasswordHashInvalid = "Validation.User.PasswordHashInvalid";
        public const string PasswordHasherNull = "Validation.User.PasswordHasherNull";
    }
}