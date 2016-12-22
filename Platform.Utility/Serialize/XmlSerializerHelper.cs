using System.IO;
using System.Xml.Serialization;

namespace SHWDTech.Platform.Utility.Serialize
{
    public class XmlSerializerHelper
    {
        public static string Serialize<T>(T target)
        {
            var stringwriter = new StringWriter();
            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stringwriter, target);
            return stringwriter.ToString();
        }

        public static T DeSerialize<T>(string jsonString)
        {
            var stringReader = new StringReader(jsonString);
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(stringReader);
        }
    }
}
