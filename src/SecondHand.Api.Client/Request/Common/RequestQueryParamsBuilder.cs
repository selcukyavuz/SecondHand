namespace SecondHand.Api.Client.Request.Common;

using System.Globalization;
using System.Runtime.Serialization;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


public static class RequestQueryParamsBuilder
{
    public static string BuildQueryParam(object request)
    {
        var fields = Enumerable.ToList(request.GetType().GetProperties());
        var query = new StringBuilder(fields.Any() ? "?" : string.Empty);
        foreach (var field in fields)
        {
            var value = field.GetValue(request);
            var name = char.ToLowerInvariant(field.Name[0]) + field.Name.Substring(1);
            if (value != null)
            {
                query.Append($"{field.Name}={Uri.EscapeDataString(FormatValue(value))}&");
            }
        }

        return query.ToString().TrimEnd('&');
    }

    private static string FormatValue(object value)
    {
        switch(value)
        {
            case DateTime time:
                return FormatDateValue(time);
            case ISet<long> enumarable:
                return FormatListValue(enumarable);
            case Enum @enum:
                return GetEnumMemberAttrValue(@enum)!;
            case decimal @decimal:
                return @decimal.ToString(new CultureInfo("en-US"));
            default :
                return value.ToString()!;
        } 
    }

    private static string FormatDateValue(DateTime value)
    {
        return value.ToString("yyyy-MM-dd'T'HH:mm:ss");
    }

    private static string FormatListValue<T>(ISet<T> value)
    {
        return string.Join(",", value);
    }

    private static string? GetEnumMemberAttrValue<T>(T enumVal)
    {
        var attr = enumVal?
            .GetType()
            .GetRuntimeField(enumVal?.ToString()!)?
            .GetCustomAttribute(typeof(EnumMemberAttribute)) as EnumMemberAttribute;
            
        if (attr != null)
            return attr.Value;

        return null;
    }
        
}
