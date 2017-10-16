using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

namespace eaivr
{
    public class XmlUtility
    {
        public static void Serialize(object item, string path)
        {
            XmlSerializer serializer = new XmlSerializer(item.GetType());
            StreamWriter writer = new StreamWriter(path);
            serializer.Serialize(writer.BaseStream, item);
            writer.Close();
        }

        public static T Deserialize<T>(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StreamReader reader = new StreamReader(path);
            T deserialized = (T)serializer.Deserialize(reader.BaseStream);
            reader.Close();
            return deserialized;
        }

        public static T DeserializeBuild<T>(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            TextAsset textAsset = (TextAsset)Resources.Load(path, typeof(TextAsset));

            using (var reader = new System.IO.StringReader(textAsset.text))
            {
                return (T)serializer.Deserialize(reader);
            }
            //XmlSerializer serializer = new XmlSerializer(typeof(T));
            //StreamReader reader = new StreamReader(path);

            //XmlDocument xmlDocument = new XmlDocument();
            //xmlDocument.LoadXml(textAsset.text);
            ////serializer.Deserialize(xmlDocument);
            //T deserialized = (T)serializer.Deserialize(reader.BaseStream);
            //reader.Close();
            //return deserialized;
        }
    }
}

