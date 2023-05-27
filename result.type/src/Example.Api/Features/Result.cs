using FluentValidation.Results;

namespace Example.Api.Features;

public readonly struct Result<TValue, TError>
{
   private readonly TValue? _value;
   private readonly TError? _error;

    public Result(TError? error)
    {
        _error = error;
        IsError = true;
        _value = default;
    }

    public Result(TValue? value)
    {
        _value = value;
        IsError = false;
        _error = default;
    }

    public bool IsError { get; }
    public bool IsSuccess => !IsError;

    public static implicit operator Result<TValue, TError>(TValue value) => new(value);
    public static implicit operator Result<TValue, TError>(TError error) => new(error);
       
    public TypedResults Match<TypedResults>(
        Func<TValue, TypedResults> success, 
        Func<TError, TypedResults> failure) => 
        !IsError ? success(_value!) : failure(_error!);
}

public class ValidationFailed
{
    public ValidationFailed(ValidationResult validationResult) => ValidationResult = validationResult;
    public ValidationFailed(string property, string message) => ValidationResult = new ValidationResult(new[] { new ValidationFailure(property, message) });
    public ValidationResult ValidationResult { get; }
     
}

public class NotFoundFailed
{
    public NotFoundFailed(Guid id) => Id = id;
    public Guid Id { get; }
}