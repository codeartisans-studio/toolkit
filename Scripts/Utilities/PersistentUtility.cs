using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using UnityEngine;

namespace Toolkit.Utilities
{
    public static class PersistentUtility
    {
        public static T LoadBinary<T>(string filename)
        {
            string filePath = Application.persistentDataPath + "/" + filename;

            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream file = File.Open(filePath, FileMode.Open))
            {
                return (T)formatter.Deserialize(file);
            }
        }

        public static void SaveBinary<T>(string filename, T data)
        {
            string filePath = Application.persistentDataPath + "/" + filename;

            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream file = File.Create(filePath))
            {
                formatter.Serialize(file, data);
            }
        }

        public static T LoadXml<T>(string filename)
        {
            string filePath = Application.persistentDataPath + "/" + filename;

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (FileStream file = File.Open(filePath, FileMode.Open))
            {
                return (T)serializer.Deserialize(file);
            }
        }

        public static void SaveXml<T>(string filename, T data)
        {
            string filePath = Application.persistentDataPath + "/" + filename;

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (FileStream file = File.Create(filePath))
            {
                serializer.Serialize(file, data);
            }
        }

        public static T FromJSON<T>(string filename)
        {
            string filePath = Application.persistentDataPath + "/" + filename;

            return JsonUtility.FromJson<T>(File.ReadAllText(filePath));
        }

        public static void FromJsonOverwrite(string filename, object objectToOverwrite)
        {
            string filePath = Application.persistentDataPath + "/" + filename;

            JsonUtility.FromJsonOverwrite(File.ReadAllText(filePath), objectToOverwrite);
        }

        public static void ToJSON(string filename, object obj, bool prettyPrint = false)
        {
            string filePath = Application.persistentDataPath + "/" + filename;

            File.WriteAllText(filePath, JsonUtility.ToJson(obj, prettyPrint));
        }
    }
}
