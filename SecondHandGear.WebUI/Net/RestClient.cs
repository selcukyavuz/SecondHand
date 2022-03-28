namespace StravaStore.Net;

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using StravaStore.Common;
using StravaStore.Exception;
using StravaStore.Response.Common;
using System.Text.Json;

public static class RestClient
{
    private static readonly HttpClient HttpClient;

    static RestClient()
    {
#if !NETSTANDARD1_3
    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
#endif
        var handler = new HttpClientHandler() { AllowAutoRedirect = false };
        HttpClient = new HttpClient(handler)
        {
            Timeout = TimeSpan.FromSeconds(15)
        };
    }

    public static T Get<T>(string url, Dictionary<string, string> headers)  where T : class
    {
       return Exchange<T>(url, HttpMethod.Get, headers,null!);
    }

    public static T Post<T>(string url, Dictionary<string, string> headers,object request)  where T : class
    {
       return Exchange<T>(url, HttpMethod.Post, headers,request);
    }

    public static T Post<T>(string url, Dictionary<string, string> headers)  where T : class
    {
        return Exchange<T>(url, HttpMethod.Post, headers, null!);
    }

    public static T Put<T>(string url, Dictionary<string, string> headers,object request)  where T : class
    {
       return Exchange<T>(url, HttpMethod.Put, headers,request);
    }

    public static void Delete<T>(string url, Dictionary<string, string> headers)  where T : class
    {
       Exchange<T>(url, HttpMethod.Delete, headers,null!);
    }

    public static T Exchange<T>(string url, HttpMethod httpMethod,Dictionary<string,string>  headers, object request)  where T : class
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
            var httpResponseMessage = HttpClient.SendAsync(requestMessage).Result;
            return HandleResponse<T>(httpResponseMessage);
        }
        catch(StravaStoreException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new StravaStoreException(ex);
        }
    }

    private static T HandleResponse<T>(HttpResponseMessage httpResponseMessage) where T : class
    {
        var apiResponse = JsonSerializer.Deserialize<T>(httpResponseMessage.Content.ReadAsStringAsync().Result, StravaStoreJsonSerializerSettings.Settings);

        if(apiResponse == null)
        {
            return default!;
        }
        // else if(apiResponse?.Errors != null)
        // {
        //     var errorResponse = apiResponse.Errors;
        //     throw new StravaStoreException(errorResponse.ErrorCode!, errorResponse.ErrorDescription!, errorResponse.ErrorGroup!);
        // }

        return apiResponse!;
    }

    public static StringContent PrepareContent(object request)
    {
        if(request == null)
        {
            return null!;
        }
        var json = JsonSerializer.Serialize(request,StravaStoreJsonSerializerSettings.Settings);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }


}