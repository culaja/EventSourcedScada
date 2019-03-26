using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.Messaging.Serialization
{
    public static class MessageSerializer
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            Converters = new List<JsonConverter> {new MaybeSerializer<string>()}
        };

        public static string Serialize(this IMessage e) => JsonConvert.SerializeObject(e, Settings);

        public static Result<IMessage> Deserialize(this string str)
        {
            try
            {
                var domainEventDto = (IMessage) JsonConvert.DeserializeObject(
                    str,
                    Settings);

                return domainEventDto.ToOkResult();
            }
            catch (JsonReaderException e)
            {
                return Result.Fail<IMessage>(e.Message);
            }
        }
    }
}