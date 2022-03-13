namespace StravaAuth.Common;

using System.Text.Json;
using System.Text.Json.Serialization;

public class StravaAuthJsonSerializerSettings
{
    public static JsonSerializerOptions Settings { get; set;} = new JsonSerializerOptions()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter() }
    };
}