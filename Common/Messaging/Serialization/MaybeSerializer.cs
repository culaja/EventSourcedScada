using System;
using Newtonsoft.Json;

namespace Common.Messaging.Serialization
{
    public sealed class MaybeSerializer<T> : JsonConverter<Maybe<T>> where T : class
    {
        public override void WriteJson(JsonWriter writer, Maybe<T> value, JsonSerializer serializer)
        {
            writer.WriteValue(value.HasValue? value.Value : null);
        }

        public override Maybe<T> ReadJson(JsonReader reader, Type objectType, Maybe<T> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return Maybe<T>.From((T)reader.Value);
        }
    }
}