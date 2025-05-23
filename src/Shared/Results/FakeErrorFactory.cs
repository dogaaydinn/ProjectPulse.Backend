using System;
using System.Collections.Generic;
using Shared.Constants;
using Shared.Results;

namespace Shared.Results
{
    public class FakeErrorFactory : IErrorFactory
    {
        public Error Create(
            string code,
            object[]? args = null,
            Dictionary<string, object>? metadata = null,
            ErrorSeverity? severity = null,
            ErrorCategory? category = null)
        {
            var cat = category ?? ErrorCategory.Domain;
            var sev = severity ?? ErrorSeverity.Medium;
            var template = $"[Fake] {code}";

            var err = Error.Create(
                code,
                template,
                category: cat,
                severity: sev,
                exception: null,
                args: args ?? Array.Empty<object>());

            if (metadata != null)
            {
                foreach (var kv in metadata)
                {
                    err = err.WithMetadata(kv.Key, kv.Value);
                }
            }

            return err;
        }

        public Error Unexpected(string message) =>
            Create(
                ErrorCodes.General.Unexpected,
                args: new object[] { message },
                severity: ErrorSeverity.Critical,
                category: ErrorCategory.Infrastructure);

        public Error Conflict(string message, object? context = null) =>
            Create(
                ErrorCodes.General.Conflict,
                args: context != null ? new object[] { context } : Array.Empty<object>(),
                severity: ErrorSeverity.Conflict,
                category: ErrorCategory.Conflict);

        public Error NotFound(string entity, object id) =>
            Create(
                $"{entity}.NotFound",
                args: new object[] { id },
                severity: ErrorSeverity.Low,
                category: ErrorCategory.NotFound);

        public Error Validation(string field, string rule, object? invalidValue) =>
            Create(
                $"Validation.{field}.{rule}",
                args: new object[] { field, invalidValue! },
                severity: ErrorSeverity.Validation,
                category: ErrorCategory.Validation);
    }
}
