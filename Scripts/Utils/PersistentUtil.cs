using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Toolkit.Utils
{
	public static class PersistentUtil
	{
		public static object Load (string filename)
		{
			string filePath = Application.persistentDataPath + "/" + filename;

			object data = null;

			if (File.Exists (filePath)) {
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file = File.Open (filePath, FileMode.Open);
				data = bf.Deserialize (file);
				file.Close ();
			}

			return data;
		}

		public static void Save (string filename, object data)
		{
			string filePath = Application.persistentDataPath + "/" + filename;

			BinaryFormatter bf = new BinaryFormatter ();
			//Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
			FileStream file = File.Create (filePath); //you can call it anything you want
			bf.Serialize (file, data);
			file.Close ();
		}
	}
}
