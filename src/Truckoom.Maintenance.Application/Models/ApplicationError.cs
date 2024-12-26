namespace Truckoom.Maintenance.Application.Models;
public class ApplicationError(int errorCode, string? service, string? method, object? data)
{
    public int ErrorCode { get; } = errorCode;
    public string? Service { get; } = service;
    public string? Method { get; } = method;
    public object? Data { get; } = data;
}