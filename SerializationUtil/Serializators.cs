using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;

namespace SerializationUtil{
    public class Serializer{
        public static MemoryStream SerializeBinary(object request){
            var serializer = new BinaryFormatter();
            var memStream = new MemoryStream();
            serializer.Serialize(memStream, request);
            return memStream;
        }

        public static object DeSerializeBinary(MemoryStream memStream){
            memStream.Position = 0;
            var deserializer = new BinaryFormatter();
            object newobj = deserializer.Deserialize(memStream);
            memStream.Close();
            return newobj;
        }

        public static MemoryStream SerializeSOAP(object request){
            var serializer = new SoapFormatter();
            var memStream = new MemoryStream();
            serializer.Serialize(memStream, request);
            return memStream;
        }

        public static object DeSerializeSOAP(MemoryStream memStream){
            object sr = null;
            var deserializer = new SoapFormatter();
            memStream.Position = 0;
            sr = deserializer.Deserialize(memStream);
            memStream.Close();
            return sr;
        }
    }
}