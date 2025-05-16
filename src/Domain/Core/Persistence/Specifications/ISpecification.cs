// ProjectPulse.Domain/Core/Persistence/Specifications/ISpecification.cs
using System.Linq.Expressions;

namespace Domain.Core.Persistence.Specifications;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    List<string> IncludeStrings { get; }
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }
    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }
    bool AsNoTracking { get; }
    bool AsSplitQuery { get; }
    bool IgnoreQueryFilters { get; }
    bool AsNoTrackingWithIdentityResolution { get; }
    bool EnableCache { get; }
    string CacheKey { get; }
}