using System.Collections.Concurrent;
using System.Reflection;

namespace Shared.Results;

public class ErrorRegistry : IErrorRegistry
{
    private readonly ConcurrentDictionary<string, ErrorDefinition> _defs = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentBag<string> _warnings = new();

    public void Register(ErrorDefinition def)
    {
        if (_defs.TryGetValue(def.Code, out var existing))
        {
            switch (existing.IsDeprecated)
            {
                case true when !def.IsDeprecated:
                    _defs[def.Code] = def;
                    break;
                case false when def.IsDeprecated:
                    _warnings.Add($"Deprecated: {def.Code} → {def.DeprecationMessage}");
                    break;
            }
        }
        else
        {
            _defs[def.Code] = def;
        }
    }

    public ErrorDefinition Get(string code)
    {
        if (_defs.TryGetValue(code, out var def))
            return def;
        throw new KeyNotFoundException($"Error code '{code}' not registered.");
    }

    public IEnumerable<string> GetDeprecationWarnings() => _warnings.ToArray();

    public void RegisterFromAssembly(Assembly assembly)
    {
        // Optionally scan for [ErrorDefinition] attributes...
    }
}
