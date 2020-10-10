using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Compilation;

namespace Toolkit.Editor
{
    public class ToolsMenu
    {
        [MenuItem("Tools/Clear All PlayerPrefs")]
        private static void ClearAllPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }

        [MenuItem("Tools/Align With Ground %t")]
        private static void AlignWithGround()
        {
            Transform[] transforms = Selection.transforms;
            foreach (Transform myTransform in transforms)
            {
                RaycastHit hit;
                if (Physics.Raycast(myTransform.position, -Vector3.up, out hit))
                {
                    Vector3 targetPosition = hit.point;
                    if (myTransform.gameObject.GetComponent<MeshFilter>() != null)
                    {
                        Bounds bounds = myTransform.gameObject.GetComponent<MeshFilter>().sharedMesh.bounds;
                        targetPosition.y += bounds.extents.y;
                    }
                    myTransform.position = targetPosition;
                    Vector3 targetRotation = new Vector3(hit.normal.x, myTransform.eulerAngles.y, hit.normal.z);
                    myTransform.eulerAngles = targetRotation;

                    EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                }
            }
        }

#if UNITY_2018_1_OR_NEWER
        [MenuItem("Tools/List Player Assemblies in Console")]
        public static void PrintAssemblyNames()
        {
            UnityEngine.Debug.Log("== Player Assemblies ==");

            Assembly[] playerAssemblies = CompilationPipeline.GetAssemblies(AssembliesType.Player);

            foreach (var assembly in playerAssemblies)
            {
                UnityEngine.Debug.Log(assembly.name);
            }
        }
#endif

        [MenuItem("Tools/Check Prototype References")]
        private static void CheckprototypeReferences()
        {
            string prototypePath = EditorUtility.OpenFolderPanel("Choose folder", Application.dataPath, "");

            if (prototypePath != "")
            {
                var files = System.IO.Directory.GetFiles(prototypePath, "*.*", System.IO.SearchOption.AllDirectories);

                //make it relative to asset folder so we can compare easily later
                prototypePath = prototypePath.Replace(Application.dataPath, "Assets");

                foreach (var s in files)
                {
                    string relativePath = s.Replace(Application.dataPath, "Assets");

                    //ignore metafile
                    if (!relativePath.EndsWith(".prefab") && !relativePath.EndsWith(".asset"))
                        continue;

                    var assets = AssetDatabase.LoadAllAssetsAtPath(relativePath);

                    foreach (var a in assets)
                    {
                        SerializedObject obj = new SerializedObject(a);
                        var prop = obj.GetIterator();

                        while (prop.NextVisible(true))
                        {
                            //monobehaviour have exposed Icon & script we do not want to test
                            if (prop.propertyType == SerializedPropertyType.ObjectReference
                                && prop.displayName != "Icon" && prop.displayName != "Script")
                            {
                                var referencedAssetPath = AssetDatabase.GetAssetPath(prop.objectReferenceValue);

                                //ignore built-in resources, only look if we are linking stuff from the projects
                                if (!referencedAssetPath.Contains("Assets/"))
                                    continue;

                                if (!referencedAssetPath.Contains(prototypePath))
                                {
                                    Debug.Log("External referenced on " + relativePath + ": " + prop.displayName + " to " + referencedAssetPath, a);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
