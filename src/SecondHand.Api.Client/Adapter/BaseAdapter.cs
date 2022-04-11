namespace SecondHand.Api.Client.Adapter;

using System.Collections.Generic;
using SecondHand.Api.Client.Request.Common;

public abstract class BaseAdapter
{
    private const int RandomStringSize = 8;
    private const string ApiVersionHeaderValue = "v1";
    private const string ApiKeyHeaderName = "x-api-key";
    private const string RandomHeaderName = "x-rnd-key";
    private const string AuthVersionHeaderName = "x-auth-version";
    private const string SignatureHeaderName = "x-signature";
    private const string RandomChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    protected readonly RequestOptions RequestOptions;

    public BaseAdapter(RequestOptions requestOptions) => RequestOptions = requestOptions;

    protected Dictionary<string, string?> CreateHeaders(string path, RequestOptions requestOptions) => CreateHttpHandlers(null, path, requestOptions);

    protected Dictionary<string, string?> CreateHeaders(object request, string path, RequestOptions requestOptions) => CreateHttpHandlers(request, path, requestOptions);

    private static Dictionary<string, string?> CreateHttpHandlers(object? request, string path, RequestOptions requestOptions)
    {
        var headers = new Dictionary<string, string?>();
        headers.Add(ApiVersionHeaderValue, ApiVersionHeaderValue);
        headers.Add(ApiKeyHeaderName, requestOptions.ApiKey);
        headers.Add(AuthVersionHeaderName, ApiVersionHeaderValue);
        return headers;
    }
}
