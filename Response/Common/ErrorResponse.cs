namespace StravaStore.Response.Common;

public class ErrorResponse
{
    public string? ErrorCode { get; set; }
    public string? ErrorDescription { get; set; }
    public string? ErrorGroup { get; set; }
}