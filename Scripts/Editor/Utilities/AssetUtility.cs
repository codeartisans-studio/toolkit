using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Toolkit
{
    public static class AssetUtility
    {
        public static void PrefabsForEach(string folder, Action<string, GameObject> action)
        {
            AssetsForEach("t:prefab", folder, action);
        }

        public static void AssetsForEach(string filter, string folder, Action<string, GameObject> action)
        {
            string[] guids = AssetDatabase.FindAssets(filter, new[] { folder });

            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);

                GameObject gameObject = (GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));

                action(path, gameObject);
            }
        }
    }
}
