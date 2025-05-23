using System.Reflection;

namespace Shared.Results;

public interface IErrorRegistry
{
    void Register(ErrorDefinition definition);
    ErrorDefinition Get(string code);
    IEnumerable<string> GetDeprecationWarnings();
    void RegisterFromAssembly(Assembly assembly); 
}