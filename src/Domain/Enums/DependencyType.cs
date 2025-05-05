using Domain.Primitives.Enums.StructuredEnum;

namespace Domain.Enums;

public sealed class DependencyType : StructuredEnum<DependencyType>
{
    public static readonly DependencyType StartToStart = new(nameof(StartToStart), 1);
    public static readonly DependencyType FinishToStart = new(nameof(FinishToStart), 2);
    public static readonly DependencyType FinishToFinish = new(nameof(FinishToFinish), 3);
    public static readonly DependencyType StartToFinish = new(nameof(StartToFinish), 4);

    private DependencyType(string name, int value) : base(name, value) { }
}