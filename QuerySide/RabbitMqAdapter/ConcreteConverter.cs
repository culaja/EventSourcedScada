using System;
using Newtonsoft.Json;

namespace RabbitMqAdapter
{
    public sealed class ConcreteConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type objectType) => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) => 
            serializer.Deserialize<T>(reader);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => 
            serializer.Serialize(writer, value);
    }
}