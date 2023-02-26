using System;

namespace GatheringEvents.Domain.Types;

public record class Error
{
    public string Message { get; }
    public ErrorCode ErrorCode { get; }

    private const string msg = "cannot execute";

    private Error(string message, ErrorCode errorCode)
    {
        Message = message;
        ErrorCode = errorCode;
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
}