using Shared.Exceptions;
using Shared.Results;
using Shared.ValueObjects;

namespace Shared.Validation
{
    public static class Ensure
    {
        public static string NotNullOrWhiteSpace(string? value, Func<Error> errorFactory)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new AppException(errorFactory());
            return value.Trim();
        }

        public static T NotNull<T>(T? value, Func<Error> errorFactory)
        {
            if (value is null)
                throw new AppException(errorFactory());
            return value;
        }

        public static Guid NotDefault(Guid value, Func<Error> errorFactory)
        {
            if (value == Guid.Empty)
                throw new AppException(errorFactory());
            return value;
        }

        public static DateTime NotInPast(DateTime value, Func<Error> errorFactory)
        {
            if (value < DateTime.UtcNow)
                throw new AppException(errorFactory());
            return value;
        }

        public static LocalizedString NotEmptyLocalized(LocalizedString? value, Func<Error> errorFactory)
        {
            if (value is null || value.IsEmpty())
                throw new AppException(errorFactory());
            return value;
        }

        public static DateRange NotEmptyDateRange(DateRange? value, Func<Error> errorFactory)
        {
            if (value is null || value.IsEmpty())
                throw new AppException(errorFactory());
            return value;
        }

        public static void ValidCondition(bool condition, Func<Error> errorFactory)
        {
            if (!condition)
                throw new AppException(errorFactory());
        }
    }
}