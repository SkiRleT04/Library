using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace Library.WEB.Utils
{
    public class Serializer
    {
        public static void ObjectToJsonBytes<T>(T serializeObject, out byte[] arrayOfBytes)
        {
            try
            {
                var jsonString = JsonConvert.SerializeObject(serializeObject);
                
                arrayOfBytes = Encoding.UTF8.GetBytes(jsonString);
            }
            catch
            {
                arrayOfBytes = null;
            }
        }

        public static void ObjectToXmlBytes<T>(T serializeObject, out byte[] arrayOfBytes)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                using (var stringWriter = new StringWriter())
                {
                    serializer.Serialize(stringWriter, serializeObject);
                    arrayOfBytes = Encoding.Unicode.GetBytes(stringWriter.ToString());
                }
            }
            catch
            {
                arrayOfBytes = null;
            }
        }

        public static void BytesJsonToObject<T>(byte[] arrayOfBytes, out T deserializeObject)
        {
            try
            {
                var stringObject = Encoding.UTF8.GetString(arrayOfBytes);
                deserializeObject = JsonConvert.DeserializeObject<T>(stringObject);
            }
            catch
            {
                deserializeObject = default(T);
            }
        }

        public static void BytesXmlToObject<T>(byte[] arrayOfBytes, out T deserializeObject)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                using (var memoryStream = new MemoryStream(arrayOfBytes))
                {
                    deserializeObject = (T)serializer.Deserialize(memoryStream);
                }
            }
            catch
            {
                deserializeObject = default(T);
            }
        }
        
    }
}