using System.Diagnostics.CodeAnalysis;

namespace SecondHand.Web.Response.Common;

public class Response<T> where T : class
{
    public T? Data { get; set; }
    public ErrorResponse? Errors { get; set; }
}

