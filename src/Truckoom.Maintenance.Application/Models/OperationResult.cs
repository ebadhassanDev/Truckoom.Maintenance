namespace Truckoom.Maintenance.Application.Models;
public class OperationResult 
{
    public bool IsSuccessful { get; protected set; }
    public object? Data { get; protected set; }
    public OperationResultType ResultType { get; protected set; }
    public ApplicationError? Error { get; protected set; }
    public static OperationResult Success(object? data = null) => new()
    {
        IsSuccessful = true,
        Data = data,
        ResultType = OperationResultType.Success,
        Error = null
    };
    public static OperationResult BadRequest(ApplicationError error, object? data = null) => new()
    {
        IsSuccessful = false,
        Data = data,
        ResultType = OperationResultType.BadRequest,
        Error = error
    };
    public static OperationResult NotFound(ApplicationError error, object? data = null) => new()
    {
        IsSuccessful = false,
        Data = data,
        ResultType = OperationResultType.ObjectNotFound,
        Error = error
    };
    public static OperationResult UnprocessableEntity(ApplicationError error, object? data = null) => new()
    {
        IsSuccessful = false,
        Data = data,
        ResultType = OperationResultType.UnprocessableEntity,
        Error = error
    };
    public static OperationResult InternalError(ApplicationError error, object? data = null) => new()
    {
        IsSuccessful = false,
        Data = data,
        ResultType = OperationResultType.InternalError,
        Error = error
    };
    public static OperationResult Forbidden(ApplicationError error, object? data = null) => new()
    {
        IsSuccessful = false,
        Data = data,
        ResultType = OperationResultType.Forbidden,
        Error = error
    };
    public T? GetData<T>()
    {
        if (this.Data is null)
        {
            return default;
        }
        return this.Data is T t ? t : default;
    }
}
public enum OperationResultType
{
    Success,
    BadRequest,
    ObjectNotFound,
    UnprocessableEntity,
    InternalError,
    Forbidden,
    Unauthorized
}