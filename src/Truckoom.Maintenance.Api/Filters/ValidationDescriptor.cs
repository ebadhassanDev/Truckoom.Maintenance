namespace Truckoom.Maintenance.Api.Filters;

using FluentValidation;

public class ValidationDescriptor
{
#pragma warning disable IDE0055
    public required int ArgumentIndex { get; init; }
    public required Type ArgumentType { get; init; }
     public required IValidator Validator { get; init; }
 #pragma warning disable IDE0055
}