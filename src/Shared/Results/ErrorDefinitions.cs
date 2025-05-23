using Shared.Constants;

namespace Shared.Results;

public static class ErrorDefinitions
{
    public static void RegisterAll(IErrorRegistry registry)
    {
        // --- System-Level Errors ---
        registry.Register(new ErrorDefinition(
            Code: ErrorCodes.General.Unexpected,
            MessageTemplate: "An unexpected error occurred: {0}",
            Severity: ErrorSeverity.Critical,
            Category: ErrorCategory.Unexpected));

        registry.Register(new ErrorDefinition(
            Code: ErrorCodes.General.Conflict,
            MessageTemplate: "Conflict: {0}",
            Severity: ErrorSeverity.Warning,
            Category: ErrorCategory.Conflict));

        // --- Validation Errors ---
        registry.Register(new ErrorDefinition(
            Code: ErrorCodes.Validation.Default,
            MessageTemplate: "Validation failed.",
            Severity: ErrorSeverity.Low,
            Category: ErrorCategory.Validation));

        registry.Register(new ErrorDefinition(
            Code: ErrorCodes.Validation.Required,
            MessageTemplate: "{0} is required.",
            Severity: ErrorSeverity.Low,
            Category: ErrorCategory.Validation));

        registry.Register(new ErrorDefinition(
            Code: ErrorCodes.Validation.InvalidFormat,
            MessageTemplate: "{0} has invalid format.",
            Severity: ErrorSeverity.Low,
            Category: ErrorCategory.Validation));
        registry.Register(new ErrorDefinition(
            Code: ErrorCodes.Validation.EmptyResult,
            MessageTemplate: "Validation result is empty but marked as invalid.",
            Severity: ErrorSeverity.Low,
            Category: ErrorCategory.Validation));

        // --- User Feature Errors ---
        registry.Register(new ErrorDefinition(
            Code: ErrorCodes.User.NotFound,
            MessageTemplate: "User not found.",
            Severity: ErrorSeverity.Medium,
            Category: ErrorCategory.NotFound));

        registry.Register(new ErrorDefinition(
            Code: ErrorCodes.User.AlreadyExists,
            MessageTemplate: "User already exists.",
            Severity: ErrorSeverity.Medium,
            Category: ErrorCategory.Conflict));

        registry.Register(new ErrorDefinition(
            Code: ErrorCodes.User.PasswordHashInvalid,
            MessageTemplate: "Password hash is invalid.",
            Severity: ErrorSeverity.Medium,
            Category: ErrorCategory.Validation));

        registry.Register(new ErrorDefinition(
            Code: ErrorCodes.User.PasswordHasherNull,
            MessageTemplate: "Password hasher cannot be null.",
            Severity: ErrorSeverity.Critical,
            Category: ErrorCategory.Unexpected));

        // --- Project Feature Errors ---
        registry.Register(new ErrorDefinition(
            Code: ErrorCodes.Project.NotFound,
            MessageTemplate: "Project not found.",
            Severity: ErrorSeverity.Medium,
            Category: ErrorCategory.NotFound));

        registry.Register(new ErrorDefinition(
            Code: ErrorCodes.Project.InvalidDateRange,
            MessageTemplate: "Invalid project date range: {0} → {1}.",
            Severity: ErrorSeverity.Medium,
            Category: ErrorCategory.Validation));

        registry.Register(new ErrorDefinition(
            Code: ErrorCodes.Project.NameAlreadyExists,
            MessageTemplate: "Project name already exists.",
            Severity: ErrorSeverity.Conflict,
            Category: ErrorCategory.Conflict));

        // --- Schedule Feature Errors ---
        registry.Register(new ErrorDefinition(
            Code: ErrorCodes.Schedule.InvalidSlot,
            MessageTemplate: "Invalid schedule slot.",
            Severity: ErrorSeverity.Medium,
            Category: ErrorCategory.Validation));

        registry.Register(new ErrorDefinition(
            Code: ErrorCodes.Schedule.Conflict,
            MessageTemplate: "Schedule conflict detected.",
            Severity: ErrorSeverity.Warning,
            Category: ErrorCategory.Conflict));

        // --- LocalizedString ValueObject Errors ---
        registry.Register(new ErrorDefinition(
            Code: ErrorCodes.LocalizedString.Required,
            MessageTemplate: "At least one localized value must be provided.",
            Severity: ErrorSeverity.Low,
            Category: ErrorCategory.Validation));

        registry.Register(new ErrorDefinition(
            Code: ErrorCodes.LocalizedString.InvalidCulture,
            MessageTemplate: "Invalid culture: {0}.",
            Severity: ErrorSeverity.Medium,
            Category: ErrorCategory.Validation));

        registry.Register(new ErrorDefinition(
            Code: ErrorCodes.LocalizedString.NoValidTranslations,
            MessageTemplate: "No valid translations were found.",
            Severity: ErrorSeverity.Low,
            Category: ErrorCategory.Validation));

        registry.Register(new ErrorDefinition(
            Code: ErrorCodes.LocalizedString.MissingCulture,
            MessageTemplate: "Localized message missing for culture: {0}.",
            Severity: ErrorSeverity.Medium,
            Category: ErrorCategory.Localization));
    }
}