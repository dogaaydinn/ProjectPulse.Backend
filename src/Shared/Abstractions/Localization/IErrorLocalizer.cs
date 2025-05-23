namespace Shared.Abstractions.Localization;

public interface IErrorLocalizer
{
    string? GetMessage(string code, object[] args);
}