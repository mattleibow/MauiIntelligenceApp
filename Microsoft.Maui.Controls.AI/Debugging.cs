using System.Text;
using System.Text.Json;
using Microsoft.Extensions.AI;

namespace Microsoft.Maui.Controls.AI;

public static class Debugging
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.General)
    {
        WriteIndented = false
    };

    public static string ToDebugString(this ChatMessage message)
    {
        if (!string.IsNullOrWhiteSpace(message.Text))
        {
            return message.Text;
        }

        var sb = new StringBuilder();

        if (message.Contents.Count > 0)
        {
            foreach (var content in message.Contents)
            {
                var contentText = FormatContent(content);
                if (!string.IsNullOrWhiteSpace(contentText))
                {
                    sb.AppendLine(contentText);
                }
            }
        }

        if (sb.Length > 0)
        {
            return sb.ToString().TrimEnd();
        }

        return "";
    }

    private static string FormatContent(AIContent content) => content switch
    {
        TextContent text when !string.IsNullOrWhiteSpace(text.Text) => text.Text!,
        FunctionCallContent call => FormatFunctionCall(call),
        FunctionResultContent result => FormatFunctionResult(result),
        _ => content.ToString() ?? string.Empty
    };

    private static string FormatFunctionCall(FunctionCallContent call)
    {
        var name = string.IsNullOrWhiteSpace(call.Name) ? call.CallId : call.Name;
        var header = string.IsNullOrWhiteSpace(name) ? "tool-call" : $"tool-call:{name}";

        if (call.Arguments is not { Count: > 0 })
        {
            return header;
        }

        var body = SerializeValue(call.Arguments);
        return string.IsNullOrWhiteSpace(body) ? header : $"{header}\n{body}";
    }

    private static string FormatFunctionResult(FunctionResultContent result)
    {
        var header = string.IsNullOrWhiteSpace(result.CallId) ? "tool-result" : $"tool-result:{result.CallId}";
        var body = SerializeValue(result.Result);
        return string.IsNullOrWhiteSpace(body) ? header : $"{header}\n{body}";
    }

    private static string SerializeValue(object? value)
    {
        if (value is null)
        {
            return string.Empty;
        }

        if (value is string s)
        {
            return s;
        }

        if (value is JsonElement jsonElement)
        {
            return jsonElement.GetRawText();
        }

        if (value is IEnumerable<KeyValuePair<string, object?>> dictionary)
        {
            return JsonSerializer.Serialize(dictionary, JsonOptions);
        }

        try
        {
            return JsonSerializer.Serialize(value, JsonOptions);
        }
        catch
        {
            return value.ToString() ?? string.Empty;
        }
    }
}
