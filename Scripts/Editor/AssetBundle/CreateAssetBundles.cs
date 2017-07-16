using UnityEngine;
using UnityEditor;

namespace Toolkit.Editor.AssetBundle
{
	public class CreateAssetBundles
	{
		[MenuItem ("Assets/Build AssetBundles/Android")]
		static void BuildAllAssetBundlesAndroid ()
		{
			BuildPipeline.BuildAssetBundles ("Assets/AssetBundles/Android", BuildAssetBundleOptions.None, BuildTarget.Android);
		}

		[MenuItem ("Assets/Build AssetBundles/iOS")]
		static void BuildAllAssetBundlesiOS ()
		{
			BuildPipeline.BuildAssetBundles ("Assets/AssetBundles/iOS", BuildAssetBundleOptions.None, BuildTarget.iOS);
		}

		[MenuItem ("Assets/Build AssetBundles/StandaloneOSXUniversal")]
		static void BuildAllAssetBundlesStandaloneOSXUniversal ()
		{
			BuildPipeline.BuildAssetBundles ("Assets/AssetBundles/StandaloneOSXUniversal", BuildAssetBundleOptions.None, BuildTarget.StandaloneOSXUniversal);
		}
	}
}