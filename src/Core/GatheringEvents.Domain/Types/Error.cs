using System;

namespace GatheringEvents.Domain.Types;

public sealed record Error
{
    public string Message { get; }
    public ErrorCode ErrorCode { get; }
    public bool IsUnhandledError { get; }
    
    private const string msg = "cannot execute";

    private Error(string message, ErrorCode errorCode, bool isUnhandledError = false)
    {
        Message = message;
        ErrorCode = errorCode;
        IsUnhandledError = isUnhandledError;
    }

    public static Error BuildNewArgumentNullException(string operation, string parameterName)
    {
        return new Error(
            $"{nameof(ArgumentNullException)}: {msg} {operation} - (Parameter `{parameterName}`)",
            ErrorCode.BadRequest
        );
    } 

    public static Error BuildNewInvalidOperationException(string operation, InvitationStatus status)
    {
        return new Error(
            $"{nameof(InvalidOperationException)}: {msg} {operation} - (Parameter `{nameof(InvitationStatus)}:{status}`)",
            ErrorCode.BadRequest
        );
    }

     public static Error BuildNewInvalidOperationException(string operation, string parameterName)
     {
        return new Error(
            $"{nameof(InvalidOperationException)}: {msg} {operation} - (Parameter `{parameterName}`)",
            ErrorCode.BadRequest
        );
     }

    public static Error BuildNewArgumentOutOfRangeException(string operation, string parameterName)
    {
        return new Error(
            $"{nameof(ArgumentOutOfRangeException)}: {msg} {operation} - (Parameter `{parameterName}`)",
            ErrorCode.BadRequest
        );
    }

    public static Error BuildNewUnhandledException(string operation, string obj, Exception exception)
    {
        return new Error(
            $"{nameof(Exception)}: {msg} {operation} - (Parameter `{obj}`)\n {exception}",
            ErrorCode.BadRequest,
            true
        );
    }
}