namespace Application.Common.Mapping.Interfaces;

public interface IMapper<in TSource, out TDestination>
{
    TDestination Map(TSource source);
}