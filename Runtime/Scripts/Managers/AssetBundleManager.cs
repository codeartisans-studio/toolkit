using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Toolkit
{
    public static class AssetBundleManager
    {
        // A dictionary to hold the AssetBundle references
        private static Dictionary<string, AssetBundleRef> dictAssetBundleRefs;

        static AssetBundleManager()
        {
            dictAssetBundleRefs = new Dictionary<string, AssetBundleRef>();
        }

        // Class with the AssetBundle reference, url and version
        private class AssetBundleRef
        {
            public AssetBundle assetBundle = null;
            public uint version;
            public string url;

            public AssetBundleRef(string strUrlIn, uint intVersionIn)
            {
                url = strUrlIn;
                version = intVersionIn;
            }
        };

        // Get an AssetBundle
        public static AssetBundle GetAssetBundle(string url, uint version)
        {
            string keyName = url + version.ToString();
            AssetBundleRef abRef;
            if (dictAssetBundleRefs.TryGetValue(keyName, out abRef))
                return abRef.assetBundle;
            else
                return null;
        }

        // Download an AssetBundle
        public static IEnumerator DownloadAssetBundle(string url, uint version)
        {
            string keyName = url + version.ToString();
            if (dictAssetBundleRefs.ContainsKey(keyName))
            {
                yield return null;
            }
            else
            {
                while (!Caching.ready)
                    yield return null;

#if UNITY_2018_1_OR_NEWER
                using (UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(url, version, 0))
#else
                using (UnityWebRequest uwr = UnityWebRequest.GetAssetBundle(url, version, 0))
#endif
                {
                    yield return uwr.SendWebRequest();

                    if (uwr.isNetworkError || uwr.isHttpError)
                    {
                        Debug.Log(uwr.error);
                    }
                    else
                    {
                        // Get downloaded asset bundle
                        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwr);

                        AssetBundleRef abRef = new AssetBundleRef(url, version);
                        abRef.assetBundle = bundle;
                        dictAssetBundleRefs.Add(keyName, abRef);
                    }
                }
            }
        }

        // Unload an AssetBundle
        public static void Unload(string url, uint version, bool allObjects)
        {
            string keyName = url + version.ToString();
            AssetBundleRef abRef;
            if (dictAssetBundleRefs.TryGetValue(keyName, out abRef))
            {
                abRef.assetBundle.Unload(allObjects);
                abRef.assetBundle = null;
                dictAssetBundleRefs.Remove(keyName);
            }
        }
    }
}
