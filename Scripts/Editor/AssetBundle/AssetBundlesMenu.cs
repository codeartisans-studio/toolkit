using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Toolkit.Editor.AssetBundle
{
	public class AssetBundlesMenu
	{
		[MenuItem ("Assets/Get AssetBundle Names")]
		private static void GetAssetBundleNames ()
		{
			var names = AssetDatabase.GetAllAssetBundleNames ();
			foreach (var name in names)
				Debug.Log ("AssetBundle: " + name);
		}

		[MenuItem ("Assets/Build AssetBundles/Android")]
		private static void BuildAssetBundlesAndroid ()
		{
			BuildPipeline.BuildAssetBundles ("Assets/AssetBundles/Android", BuildAssetBundleOptions.None, BuildTarget.Android);
		}

		[MenuItem ("Assets/Build AssetBundles/iOS")]
		private static void BuildAssetBundlesiOS ()
		{
			BuildPipeline.BuildAssetBundles ("Assets/AssetBundles/iOS", BuildAssetBundleOptions.None, BuildTarget.iOS);
		}

		[MenuItem ("Assets/Build AssetBundles/StandaloneOSXUniversal")]
		private static void BuildAssetBundlesStandaloneOSXUniversal ()
		{
			BuildPipeline.BuildAssetBundles ("Assets/AssetBundles/StandaloneOSXUniversal", BuildAssetBundleOptions.None, BuildTarget.StandaloneOSXUniversal);
		}
	}
}