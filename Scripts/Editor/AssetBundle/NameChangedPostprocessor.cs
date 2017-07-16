using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Toolkit.Editor.AssetBundle
{
	public class NameChangedPostprocessor : AssetPostprocessor
	{
		public void OnPostprocessAssetbundleNameChanged (string assetPath, string previousAssetBundleName, string newAssetBundleName)
		{
			Debug.Log ("Asset " + assetPath + " has been moved from assetBundle " + previousAssetBundleName + " to assetBundle " + newAssetBundleName + ".");
		}

	}
}