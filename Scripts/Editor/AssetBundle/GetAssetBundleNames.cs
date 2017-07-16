using UnityEngine;
using UnityEditor;

namespace Toolkit.Editor.AssetBundle
{
	public class GetAssetBundleNames
	{
		[MenuItem ("Assets/Get AssetBundle names")]
		static void GetNames ()
		{
			var names = AssetDatabase.GetAllAssetBundleNames ();
			foreach (var name in names)
				Debug.Log ("AssetBundle: " + name);
		}
	}
}