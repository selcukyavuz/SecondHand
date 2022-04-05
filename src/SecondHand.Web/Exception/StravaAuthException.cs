namespace SecondHand.Web.Exception;

using System;

public class SecondHandWebException : Exception
{
    private const string GeneralErrorCode = "0";
    private const string GeneralErrorDesction = "An error occurred.";
    private const string GeneralErrorGroup = "Unknown";
    public string ErrorCode { get;}
    public string ErrorDescription { get;}
    public string ErrorGroup { get;}
    public SecondHandWebException(string errorCode, string errorDescription, string errorGroup) : base(errorDescription)
    {
        ErrorCode = errorCode;
        ErrorDescription = errorDescription;
        ErrorGroup = errorGroup;
    }

    public SecondHandWebException(Exception exception) : base(exception.Message, exception)
    {
        ErrorCode = GeneralErrorCode;
        ErrorDescription = GeneralErrorDesction;
        ErrorGroup = GeneralErrorGroup;
    }
}