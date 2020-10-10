using UnityEngine;
using UnityEditor;

namespace Toolkit.Editor
{
    [CustomPropertyDrawer(typeof(RenameAttribute))]
    public class RenameEditor : PropertyDrawer
    {
        private RenameAttribute RenameAttribute
        {
            get
            {
                return attribute as RenameAttribute;
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, new GUIContent(RenameAttribute.name));
        }
    }
}