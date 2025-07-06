using System.Xml.Serialization;
using System.Reflection;
using BluNoro.Core.Common.DataObjects;
using BluNoro.Core.Common.Abstracts;

namespace BluNoro.Core.Common.Serilization
{
    public class MessageSerializer
    {
        private static XmlSerializer CreateServerSerializer()
        {
            var baseType = typeof(MessageBaseServer);
            var derivedTypes = Assembly.GetAssembly(baseType)
                .GetTypes()
                .Where(t => t.IsSubclassOf(baseType))
                .Concat(new[] { typeof(AnonymousUser), typeof(AnonymousConnection), typeof(ConnectionStatus), typeof(MessageBaseBroadcast) }) // Add AnonymousUser explicitly
                .ToArray();

            return new XmlSerializer(baseType, derivedTypes);
        }

        private XmlSerializer serverSerializer = CreateServerSerializer();


        private static XmlSerializer CreateClientSerializer()
        {
            var baseType = typeof(MessageBaseClient);
            var derivedTypes = Assembly.GetAssembly(baseType)
                .GetTypes()
                .Where(t => t.IsSubclassOf(baseType))
                .Concat(new[] { typeof(AnonymousUser), typeof(AnonymousConnection), typeof(ConnectionStatus) }) // Add AnonymousUser explicitly
                .ToArray();

            return new XmlSerializer(baseType, derivedTypes);
        }

        private XmlSerializer clientSerializer = CreateClientSerializer();


        public string SerializeMessageToString(MessageBaseServer messageBaseServer)
        {
            using (StringWriter writer = new StringWriter())
            {
                serverSerializer.Serialize(writer, messageBaseServer);
                return writer.ToString();
            }
        }

        public string SerializeMessageToString(MessageBaseClient messageBaseClient)
        {
            using (StringWriter writer = new StringWriter())
            {
                clientSerializer.Serialize(writer, messageBaseClient);
                return writer.ToString();
            }
        }

        public MessageBaseServer DeserializeServerMessageFromString(string messageString)
        {
            using (StringReader reader = new StringReader(messageString))
            {
                return (MessageBaseServer)serverSerializer.Deserialize(reader);
            }
        }

        public MessageBaseClient DeserializeClientMessageFromString(string messageString)
        {
            using (StringReader reader = new StringReader(messageString))
            {
                return (MessageBaseClient)clientSerializer.Deserialize(reader);
            }
        }


    }

    public static class MessageBaseExtensions
    {
        private static readonly MessageSerializer Serializer = new MessageSerializer();

        public static string SerilizeMe(this MessageBaseClient client)
        {
            return Serializer.SerializeMessageToString(client);
        }

        public static string SerilizeMe(this MessageBaseServer client)
        {
            return Serializer.SerializeMessageToString(client);
        }
    }


}
