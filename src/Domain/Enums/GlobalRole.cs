using Domain.Primitives.Enums.StructuredEnum;

namespace Domain.Enums;

public sealed class GlobalRole : StructuredEnum<GlobalRole>
{
    public static readonly GlobalRole Admin = new(nameof(Admin), 1);
    public static readonly GlobalRole Manager = new(nameof(Manager), 2);
    public static readonly GlobalRole Member = new(nameof(Member), 3);
    public static readonly GlobalRole Guest = new(nameof(Guest), 4);
    public static readonly GlobalRole User = new(nameof(User), 5);

    private GlobalRole(string name, int value) : base(name, value) { }
}