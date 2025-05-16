using Domain.Core.Persistence.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Common.Specification;

public static class EntitySpecificationEvaluator
{
    public static IQueryable<T> ApplySpecification<T>(
        this IQueryable<T> query,
        ISpecification<T> specification,
        bool evaluateCriteriaOnly = false)
        where T : class
    {
        if (specification.Criteria is not null)
            query = query.Where(specification.Criteria);

        if (evaluateCriteriaOnly) return query;
        
        query = specification.Includes
            .Aggregate(query, (current, include) => current.Include(include));

        query = specification.IncludeStrings
            .Aggregate(query, (current, include) => current.Include(include));
        
        if (specification.AsNoTracking)
            query = query.AsNoTracking();
        if (specification.AsNoTrackingWithIdentityResolution)
            query = query.AsNoTrackingWithIdentityResolution();

        if (specification.EnableCache)
            query = query.TagWith($"CacheKey:{specification.CacheKey}");
        

        if (specification.AsSplitQuery)
            query = query.AsSplitQuery();

        if (specification.IgnoreQueryFilters)
            query = query.IgnoreQueryFilters();
        
        if (specification.OrderBy is not null)
            query = query.OrderBy(specification.OrderBy);
        else if (specification.OrderByDescending is not null)
            query = query.OrderByDescending(specification.OrderByDescending);
        
        if (specification.IsPagingEnabled)
            query = query.Skip(specification.Skip).Take(specification.Take);

        return query;
    }
}