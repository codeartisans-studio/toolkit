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
            public string url;

            public AssetBundleRef(string strUrlIn)
            {
                url = strUrlIn;
            }
        };

        // Get an AssetBundle
        public static AssetBundle GetAssetBundle(string url)
        {
            string keyName = url;
            AssetBundleRef abRef;
            if (dictAssetBundleRefs.TryGetValue(keyName, out abRef))
                return abRef.assetBundle;
            else
                return null;
        }

        // Download an AssetBundle
        public static IEnumerator DownloadAssetBundle(string url)
        {
            string keyName = url;
            if (dictAssetBundleRefs.ContainsKey(keyName))
            {
                yield return null;
            }
            else
            {
                using (UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(url))
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

                        AssetBundleRef abRef = new AssetBundleRef(url);
                        abRef.assetBundle = bundle;
                        dictAssetBundleRefs.Add(keyName, abRef);
                    }
                }
            }
        }

        // Unload an AssetBundle
        public static void Unload(string url, bool allObjects)
        {
            string keyName = url;
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
