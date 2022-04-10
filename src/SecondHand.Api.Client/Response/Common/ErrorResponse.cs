namespace SecondHand.Api.Client.Response.Common;

public class ErrorResponse
{
    public string? ErrorCode { get; set; }
    public string? ErrorDescription { get; set; }
    public string? ErrorGroup { get; set; }
}