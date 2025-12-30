
namespace Monetra.Domain.BackOffice.Commum.Abstraction;

public class Result
{
    public bool IsSuccess { get; }
    public Error Error { get; }

    protected Result(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }
    protected Result(Error error)
    {
        IsSuccess = false;
        Error = error;
    }

    public static  Result Success() =>
        new(true);

    public static Result Failure(Error error) =>
        new(error);
}