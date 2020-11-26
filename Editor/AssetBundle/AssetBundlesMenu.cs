using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace Toolkit.Editor
{
    public static class AssetBundlesMenu
    {
        [MenuItem("Assets/Get AssetBundle Names")]
        public static void GetAssetBundleNames()
        {
            var names = AssetDatabase.GetAllAssetBundleNames();
            foreach (var name in names)
                Debug.Log("AssetBundle: " + name);
        }

        [MenuItem("Assets/Build AssetBundles/StandaloneWindows")]
        public static void BuildAssetBundlesStandaloneWindows()
        {
            string assetBundleDirectory = "Assets/AssetBundles/StandaloneWindows";
            if (!Directory.Exists(assetBundleDirectory))
            {
                Directory.CreateDirectory(assetBundleDirectory);
            }
            BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
        }

        [MenuItem("Assets/Build AssetBundles/StandaloneWindows64")]
        public static void BuildAssetBundlesStandaloneWindows64()
        {
            string assetBundleDirectory = "Assets/AssetBundles/StandaloneWindows64";
            if (!Directory.Exists(assetBundleDirectory))
            {
                Directory.CreateDirectory(assetBundleDirectory);
            }
            BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
        }

        [MenuItem("Assets/Build AssetBundles/StandaloneOSX")]
        public static void BuildAssetBundlesStandaloneOSX()
        {
            string assetBundleDirectory = "Assets/AssetBundles/StandaloneOSX";
            if (!Directory.Exists(assetBundleDirectory))
            {
                Directory.CreateDirectory(assetBundleDirectory);
            }
            BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneOSX);
        }

        [MenuItem("Assets/Build AssetBundles/Android")]
        public static void BuildAssetBundlesAndroid()
        {
            string assetBundleDirectory = "Assets/AssetBundles/Android";
            if (!Directory.Exists(assetBundleDirectory))
            {
                Directory.CreateDirectory(assetBundleDirectory);
            }
            BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.Android);
        }

        [MenuItem("Assets/Build AssetBundles/iOS")]
        public static void BuildAssetBundlesiOS()
        {
            string assetBundleDirectory = "Assets/AssetBundles/iOS";
            if (!Directory.Exists(assetBundleDirectory))
            {
                Directory.CreateDirectory(assetBundleDirectory);
            }
            BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.iOS);
        }
    }
}