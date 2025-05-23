using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Shared.Results;

namespace Shared.Exceptions
{
    public class AppException : Exception
    {
        public string Code { get; }
        public string? Details { get; }
        public IReadOnlyDictionary<string, object> Metadata { get; }

        public AppException(
            string code,
            string message,
            string? details = null,
            Dictionary<string, object>? metadata = null,
            Exception? innerException = null)
            : base(message, innerException)
        {
            Code = code ?? throw new ArgumentNullException(nameof(code));
            Details = details;
            Metadata = new ReadOnlyDictionary<string, object>(
                metadata != null ? new Dictionary<string, object>(metadata) : new Dictionary<string, object>());
        }

        public AppException(Error error)
            : this(
                error.Code,
                error.GetLocalizedMessage(),
                error.Exception?.Message,
                error.Metadata.ToDictionary(kv => kv.Key, kv => kv.Value),
                error.Exception)
        {
        }

        public AppException WithMetadata(string key, object value)
        {
            var dict = Metadata.ToDictionary(kv => kv.Key, kv => kv.Value);
            dict[key] = value;
            return new AppException(Code, Message, Details, dict, InnerException);
        }

        public AppException WithDetails(string details)
            => new AppException(Code, Message, details, Metadata.ToDictionary(kv => kv.Key, kv => kv.Value), InnerException);

        public override string ToString()
        {
            var metaStr = string.Join(", ", Metadata.Select(kv => $"{kv.Key}: {kv.Value}"));
            return $"[Error: {Code}] {Message} (Details: {Details ?? string.Empty}) Metadata: {{{metaStr}}}";
        }
    }
}