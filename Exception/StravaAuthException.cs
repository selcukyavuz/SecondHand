namespace StravaAuth.Exception;

using System;

public class StravaAuthException : Exception
{
    private const string GeneralErrorCode = "0";
    private const string GeneralErrorDesction = "An error occurred.";
    private const string GeneralErrorGroup = "Unknown";
    public string ErrorCode { get;}
    public string ErrorDescription { get;}
    public string ErrorGroup { get;}
    public StravaAuthException(string errorCode, string errorDescription, string errorGroup) : base(errorDescription)
    {
        ErrorCode = errorCode;
        ErrorDescription = errorDescription;
        ErrorGroup = errorGroup;
    }

    public StravaAuthException(Exception exception) : base(exception.Message, exception)
    {
        ErrorCode = GeneralErrorCode;
        ErrorDescription = GeneralErrorDesction;
        ErrorGroup = GeneralErrorGroup;
    }
}