using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using Toolkit.Cameras;

namespace Toolkit
{
    public class TransformMenu
    {
        [MenuItem("CONTEXT/Transform/Reset Position X")]
        private static void ResetPositionX(MenuCommand menuCommand)
        {
            Transform transform = menuCommand.context as Transform;

            if (transform.position.x != 0)
            {
                transform.position = new Vector3(0, transform.position.y, transform.position.z);

                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
        }

        [MenuItem("CONTEXT/Transform/Reset Position Y")]
        private static void ResetPositionY(MenuCommand menuCommand)
        {
            Transform transform = menuCommand.context as Transform;

            if (transform.position.y != 0)
            {
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);

                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
        }

        [MenuItem("CONTEXT/Transform/Reset Position Z")]
        private static void ResetPositionZ(MenuCommand menuCommand)
        {
            Transform transform = menuCommand.context as Transform;

            if (transform.position.z != 0)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);

                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
        }

        [MenuItem("CONTEXT/Transform/Scale To Texture Size")]
        private static void ScaleToTextureSize(MenuCommand menuCommand)
        {
            Transform transform = menuCommand.context as Transform;

            Camera camera = Camera.main;

            float pixelsPerUnit = 1;

            SpriteRenderer spriteRenderer = transform.GetComponent<SpriteRenderer>();

            if (spriteRenderer && spriteRenderer.sprite)
            {
                pixelsPerUnit = spriteRenderer.sprite.pixelsPerUnit;
            }

            Vector2 baseResolution = camera.GetComponent<CameraAdjustment>().baseResolution;
            float distance = transform.position.z - camera.transform.position.z;

            float scale = Mathf.Tan(camera.fieldOfView / 2 * Mathf.Deg2Rad) * distance / (baseResolution.y * 100 / 2 / pixelsPerUnit);
            Vector3 newScale = new Vector3(scale, scale, transform.localScale.z);

            if (transform.localScale != newScale)
            {
                transform.localScale = newScale;

                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
        }

        // Note that we pass the same path, and also pass "true" to the second argument.
        [MenuItem("CONTEXT/Transform/Scale To Texture Size", true)]
        private static bool ScaleToTextureSizeValidation(MenuCommand menuCommand)
        {
            Transform transform = menuCommand.context as Transform;

            Camera camera = Camera.main;

            if (camera)
            {
                bool isPerspective = !camera.orthographic;
                bool hasRendererComponent = transform.GetComponent<Renderer>() != null;
                bool hasCameraAdjustmentScript = camera.GetComponent<CameraAdjustment>() != null;

                return isPerspective && hasRendererComponent && hasCameraAdjustmentScript;
            }

            return false;
        }
    }
}
