namespace StravaStore.Exception;

using System;

public class StravaStoreException : Exception
{
    private const string GeneralErrorCode = "0";
    private const string GeneralErrorDesction = "An error occurred.";
    private const string GeneralErrorGroup = "Unknown";
    public string ErrorCode { get;}
    public string ErrorDescription { get;}
    public string ErrorGroup { get;}
    public StravaStoreException(string errorCode, string errorDescription, string errorGroup) : base(errorDescription)
    {
        ErrorCode = errorCode;
        ErrorDescription = errorDescription;
        ErrorGroup = errorGroup;
    }

    public StravaStoreException(Exception exception) : base(exception.Message, exception)
    {
        ErrorCode = GeneralErrorCode;
        ErrorDescription = GeneralErrorDesction;
        ErrorGroup = GeneralErrorGroup;
    }
}