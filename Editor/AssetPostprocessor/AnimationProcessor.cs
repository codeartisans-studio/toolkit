using UnityEngine;
using UnityEditor;

// Disable import of materials if the file contains
// the @ sign marking it as an animation.
public class AnimationProcessor : AssetPostprocessor
{
    void OnPreprocessModel()
    {
        if (assetPath.Contains("@"))
        {
            ModelImporter modelImporter = assetImporter as ModelImporter;
#if UNITY_2019_3_OR_NEWER
            modelImporter.materialImportMode = ModelImporterMaterialImportMode.None;
#else
            modelImporter.importMaterials = false;
# endif
        }
    }
}