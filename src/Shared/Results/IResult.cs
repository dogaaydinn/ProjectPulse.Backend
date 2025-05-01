namespace Shared.Results;

public interface IResult
{
    bool IsSuccess { get; }
    List<Error> Errors { get; }
    bool IsFailure => !IsSuccess;
}