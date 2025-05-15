using Shared.Base;
using Shared.Exceptions;
using Shared.Validation;

namespace Domain.Modules.Users.Entities;

public class UserPreference : BaseEntity
{
    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;

    public string Language { get; private set; } = "en";
    public string TimeZone { get; private set; } = "UTC";
    public string Theme { get; private set; } = "light";
    public bool ReceiveNotifications { get; private set; } = true;

    protected UserPreference() { }

    public UserPreference(Guid userId, string language = "en", string timeZone = "UTC", string theme = "light", bool receiveNotifications = true)
    {
        Guard.AgainstDefaultGuid(userId, "Validation.UserPreference.UserId", "User ID is required.");
        SetLanguage(language);
        SetTimeZone(timeZone);
        SetTheme(theme);

        UserId = userId;
        ReceiveNotifications = receiveNotifications;
    }

    public void SetLanguage(string language)
    {
        Guard.AgainstNullOrEmpty(language, "Validation.UserPreference.Language", "Language cannot be empty.");
        Language = language;
    }

    public void SetTimeZone(string timeZone)
    {
        Guard.EnsureNotNullOrWhiteSpace(timeZone, "Validation.UserPreference.TimeZone", "Time zone cannot be empty.");
        TimeZone = timeZone;
    }

    public void SetTheme(string theme)
    {
        if (theme != "light" && theme != "dark")
            throw new AppException("Validation.UserPreference.Theme", "Theme must be 'light' or 'dark'.");
        Theme = theme;
    }

    public void ToggleNotifications(bool receive)
    {
        ReceiveNotifications = receive;
    }
}