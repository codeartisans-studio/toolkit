using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using UnityEngine;

namespace Toolkit.Utils
{
	public static class PersistentUtil
	{
		public static T LoadBinary<T> (string filename) where T : class
		{
			string filePath = Application.persistentDataPath + "/" + filename;

			T data = null;

			if (File.Exists (filePath)) {
				BinaryFormatter formatter = new BinaryFormatter ();

				using (FileStream file = File.Open (filePath, FileMode.Open)) {
					data = (T)formatter.Deserialize (file);
				}
			}

			return data;
		}

		public static void SaveBinary<T> (string filename, T data) where T : class
		{
			string filePath = Application.persistentDataPath + "/" + filename;

			BinaryFormatter formatter = new BinaryFormatter ();

			using (FileStream file = File.Create (filePath)) {
				formatter.Serialize (file, data);
			}
		}

		public static T LoadXml<T> (string filename) where T : class
		{
			string filePath = Application.persistentDataPath + "/" + filename;

			T data = null;

			if (File.Exists (filePath)) {
				XmlSerializer serializer = new XmlSerializer (typeof(T));

				using (FileStream file = File.Open (filePath, FileMode.Open)) {
					data = (T)serializer.Deserialize (file);
				}
			}

			return data;
		}

		public static void SaveXml<T> (string filename, T data) where T : class
		{
			string filePath = Application.persistentDataPath + "/" + filename;

			XmlSerializer serializer = new XmlSerializer (typeof(T));

			using (FileStream file = File.Create (filePath)) {
				serializer.Serialize (file, data);
			}
		}
	}
}
