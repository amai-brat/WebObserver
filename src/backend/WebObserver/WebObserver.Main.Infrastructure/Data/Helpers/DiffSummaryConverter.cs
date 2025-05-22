using System.Text.Json;
using System.Text.Json.Serialization;
using WebObserver.Main.Domain.Base;
using WebObserver.Main.Domain.Text;
using WebObserver.Main.Domain.YouTubePlaylist;

namespace WebObserver.Main.Infrastructure.Data.Helpers;

public class DiffSummaryConverter : JsonConverter<DiffSummary>
{
    public override DiffSummary Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        if (root.TryGetProperty("$type", out var typeProp))
        {
            var typeDiscriminator = typeProp.GetString();
            return typeDiscriminator switch
            {
                nameof(TextDiffSummary) => Deserialize<TextDiffSummary>(root, options),
                nameof(YouTubePlaylistDiffSummary) => Deserialize<YouTubePlaylistDiffSummary>(root, options),
                _ => throw new JsonException($"Unknown type discriminator: {typeDiscriminator}")
            };
        }

        // Fallback: Check for properties unique to YouTubePlaylistDiffSummary
        if (root.TryGetProperty("Changed", out _) || root.TryGetProperty("Unavailable", out _))
            return Deserialize<YouTubePlaylistDiffSummary>(root, options);
        return Deserialize<TextDiffSummary>(root, options);
    }

    private static T Deserialize<T>(JsonElement element, JsonSerializerOptions options) where T : DiffSummary
    {
        var json = element.GetRawText();
        return JsonSerializer.Deserialize<T>(json, options)!;
    }

    public override void Write(Utf8JsonWriter writer, DiffSummary value, JsonSerializerOptions options)
    {
        var obj = JsonSerializer.SerializeToNode(value, value.GetType(), options)!.AsObject();
        obj.Add("$type", value.GetType().Name);
        
        obj.WriteTo(writer);
    }
}