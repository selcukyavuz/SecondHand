namespace SecondHand.Api.Client.Common;

using System.Text.Json;
using System.Text.Json.Serialization;

public class JsonSerializerSettings
{
    public static JsonSerializerOptions Settings { get; set;} = new JsonSerializerOptions()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter() }
    };
}