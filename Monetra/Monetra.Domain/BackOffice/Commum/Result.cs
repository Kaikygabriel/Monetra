using Monetra.Domain.BackOffice.Commum.Abstraction;

namespace Monetra.Domain.BackOffice.Commum;

public class Result<T> : Result
{   
    public T Value { get; }

    private Result(Error error) : base(error)
    {
    }

    private Result(T value) : base(true)
    {
        Value = value;
    }

    public static Result<T> Sucess(T value) => new Result<T>(value);
    public static Result<T> Failure(Error error) => new Result<T>(error);
}