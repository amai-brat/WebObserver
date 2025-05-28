using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebObserver.Main.API.Helpers;

public class ObjectConverter<T> : JsonConverter<T> 
    where T : class
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // предполагается, что конвертер только для отправки на фронт
        throw new InvalidOperationException();
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize<object>(writer, value, options);
    }
}