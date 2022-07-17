namespace SecondHand.Api.Client.Adapter;

using System.Collections.Generic;
using SecondHand.Api.Client.Request.Common;

public abstract class BaseAdapter
{
    private const string ApiVersionHeaderValue = "v1";
    private const string ApiKeyHeaderName = "x-api-key";
    private const string AuthVersionHeaderName = "x-auth-version";
    protected readonly RequestOptions RequestOptions;

    protected BaseAdapter(RequestOptions requestOptions) => RequestOptions = requestOptions;

    protected static Dictionary<string, string?> CreateHeaders(RequestOptions requestOptions)
    {
        return new Dictionary<string, string?>
        {
            { ApiVersionHeaderValue, ApiVersionHeaderValue },
            { ApiKeyHeaderName, requestOptions.ApiKey },
            { AuthVersionHeaderName, ApiVersionHeaderValue }
        };
    }
}
