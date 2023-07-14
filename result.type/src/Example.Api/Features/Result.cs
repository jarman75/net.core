namespace Example.Api.Features;

public readonly struct Result<TValue, TError> where TError : ErrorResult
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


public class ErrorResult
{
    public ErrorResult(ErrorCode errorCode, string internalErrorMessage)
    {
        ErrorCode = errorCode;
        InternalErrorMessage = internalErrorMessage;
    }

    public ErrorCode ErrorCode { get; set; }
    public string? ErrorCodeDescription => Enum.GetName(ErrorCode);
    public string InternalErrorMessage { get; set; }
}


public enum ErrorCode
{
    Unauthorized = 100,  //generice unauthorized    
    
    Forbidden = 200,

    NotFound = 300, //generic not found   
    
    ValidationError = 400, //generic validation error   
    
}