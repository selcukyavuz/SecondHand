namespace SecondHand.Api.Client.Net;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using SecondHand.Api.Client.Exception;
using System.Text.Json;

public static class RestClient
{
    private static readonly HttpClient HttpClient = GetHttpClient();

    private static HttpClient GetHttpClient() => new HttpClient(new HttpClientHandler() { AllowAutoRedirect = false })
    {
        Timeout = TimeSpan.FromSeconds(15)
    };

    public static T Get<T>(string url, Dictionary<string, string> headers) where T : class => Exchange<T>(url, HttpMethod.Get, headers, null!);

    public static T Post<T>(string url, Dictionary<string, string> headers, object request) where T : class => Exchange<T>(url, HttpMethod.Post, headers, request);

    public static T Post<T>(string url, Dictionary<string, string> headers) where T : class => Exchange<T>(url, HttpMethod.Post, headers, null!);

    public static T Put<T>(string url, Dictionary<string, string> headers, object request) where T : class => Exchange<T>(url, HttpMethod.Put, headers, request);

    public static void Delete<T>(string url, Dictionary<string, string> headers) where T : class => Exchange<T>(url, HttpMethod.Delete, headers, null!);

    public static T Exchange<T>(string url, HttpMethod httpMethod, Dictionary<string, string> headers, object request)  where T : class
    {
        try
        {
            var requestMessage = new HttpRequestMessage
            {
                Method = httpMethod,
                RequestUri = new Uri(url),
                Content = PrepareContent(request)
            };

            foreach (var header in headers)
            {
                requestMessage.Headers.Add(header.Key, header.Value);
            }

            using var httpResponseMessage = HttpClient.SendAsync(requestMessage).Result;
            return HandleResponse<T>(httpResponseMessage);
        }
        catch(SecondHandWebException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new SecondHandWebException(ex);
        }
    }

    private static T HandleResponse<T>(HttpResponseMessage httpResponseMessage) where T : class
    {
        if (JsonSerializer.Deserialize<T>(
            httpResponseMessage.Content.ReadAsStringAsync().Result,
            Common.JsonSerializerSettings.Settings) == null)
        {
            return default!;
        }

        return JsonSerializer.Deserialize<T>(
            httpResponseMessage.Content.ReadAsStringAsync().Result,
            Common.JsonSerializerSettings.Settings)!;
    }

    public static StringContent PrepareContent(object request)
    {
        if(request == null)
        {
            return null!;
        }
        var json = JsonSerializer.Serialize(request,SecondHand.Api.Client.Common.JsonSerializerSettings.Settings);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
}