using Domain.Modules.Tasks.Entities;
using Shared.Base;
using Shared.Constants;
using Shared.Exceptions;
using Shared.Validation;
using Shared.ValueObjects;

namespace Domain.Modules.Projects.Entities;

public class Label : BaseEntity
{
    public LocalizedString Name { get; private set; } = null!;
    public string? Color { get; private set; }

    public ICollection<TaskLabel> TaskLabels { get; private set; } = new List<TaskLabel>();

    protected Label() { }

    public Label(LocalizedString name, string? color = null)
    {
        SetName(name);
        SetColor(color);
    }

    public void SetName(LocalizedString name)
    {
        Guard.Against.NullOrEmpty(name, ErrorCodes.Validation, ValidationMessages.Label.NameRequired);
        Name = name;
    }

    public void SetColor(string? color)
    {
        // İleride hex renk kontrolü veya enum ile renk seçimi eklenecekse burada yapılır
        Color = color?.Trim();
    }

    public void Rename(LocalizedString newName) => SetName(newName);
}