using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Toolkit
{
    public class AlignPositionWindow : EditorWindow
    {
        private bool alignToX = true;
        private bool alignToY = true;
        private bool alignToZ = true;
        private bool perspectiveAlign = false;
        private string selected = "";
        private string alignTo = "";

        [MenuItem("Window/Align Position")]
        private static void Init()
        {
            EditorWindow window = EditorWindow.GetWindow(typeof(AlignPositionWindow));
            window.titleContent = new GUIContent("Align Position");
        }

        void OnInspectorUpdate()
        {
            // Call Repaint on OnInspectorUpdate as it repaints the windows
            // less times as if it was OnGUI/Update
            Repaint();
        }

        void OnGUI()
        {
            GUILayout.Label("Select various Objects in the Hierarchy view");

            selected = Selection.activeTransform ? Selection.activeTransform.name : "";
            alignTo = "";

            foreach (Transform t in Selection.transforms)
            {
                if (t.GetInstanceID() != Selection.activeTransform.GetInstanceID())
                {
                    alignTo += t.name + " ";
                }
            }

            EditorGUILayout.LabelField("Align: ", alignTo);
            EditorGUILayout.LabelField("With: ", selected);

            alignToX = EditorGUILayout.Toggle("X", alignToX);
            alignToY = EditorGUILayout.Toggle("Y", alignToY);
            alignToZ = EditorGUILayout.Toggle("Z", alignToZ);

            if (alignToZ)
            {
                perspectiveAlign = false;
            }

            perspectiveAlign = EditorGUILayout.Toggle("Perspective Align", perspectiveAlign);

            if (perspectiveAlign)
            {
                alignToZ = false;
            }

            if (GUILayout.Button("Align"))
            {
                Align();
            }
        }

        private void Align()
        {
            if (selected == "" || alignTo == "")
            {
                Debug.LogWarning("No objects selected to align");
            }

            foreach (Transform t in Selection.transforms)
            {
                Vector3 alignementPosition = Selection.activeTransform.position;
                Vector3 newPosition;

                float coff = 1;

                if (perspectiveAlign)
                {
                    float distance = t.position.z - Camera.main.transform.position.z;
                    float alignementDistance = alignementPosition.z - Camera.main.transform.position.z;

                    coff = distance / alignementDistance;
                }

                newPosition.x = alignToX ? alignementPosition.x * coff : t.position.x;
                newPosition.y = alignToY ? alignementPosition.y * coff : t.position.y;
                newPosition.z = alignToZ ? alignementPosition.z : t.position.z;

                if (t.position != newPosition)
                {
                    t.position = newPosition;

                    EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                }
            }
        }
    }
}
