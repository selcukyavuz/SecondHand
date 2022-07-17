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
        var fields = request.GetType().GetProperties().ToList();
        var query = new StringBuilder(fields.Count > 0 ? "?" : string.Empty);
        foreach (var field in fields)
        {
            var value = field.GetValue(request);
            if (value != null)
            {
                query.Append(field.Name).Append('=').Append(Uri.EscapeDataString(FormatValue(value))).Append('&');
            }
        }

        return query.ToString().TrimEnd('&');
    }

    private static string FormatValue(object value)
    {
        return value switch
        {
            DateTime time => FormatDateValue(time),
            ISet<long> enumerable => FormatListValue(enumerable),
            Enum @enum => GetEnumMemberAttrValue(@enum)!,
            decimal @decimal => @decimal.ToString(new CultureInfo("en-US")),
            _ => value.ToString()!,
        };
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
        if (enumVal?
            .GetType()
            .GetRuntimeField(enumVal?.ToString()!)?
            .GetCustomAttribute(typeof(EnumMemberAttribute)) is EnumMemberAttribute attr)
        {
            return attr.Value;
        }

        return null;
    }
}
