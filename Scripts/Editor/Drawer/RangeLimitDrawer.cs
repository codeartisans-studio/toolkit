using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using RangeInt = Toolkit.RangeInt;

namespace Toolkit.Editor
{
    [CustomPropertyDrawer(typeof(RangeIntLimitAttribute))]
    public class RangeIntLimitDrawer : PropertyDrawer
    {
        private const float FieldWidth = 30f;
        private const float SliderPadding = 5f;

        private RangeIntLimitAttribute RangeIntLimit
        {
            get
            {
                return attribute as RangeIntLimitAttribute;
            }
        }

        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.type == typeof(RangeInt).Name)
            {
                SerializedProperty minValue = property.FindPropertyRelative("minValue");
                SerializedProperty maxValue = property.FindPropertyRelative("maxValue");

                float min = minValue.intValue;
                float max = maxValue.intValue;

                position = EditorGUI.PrefixLabel(position, label);

                // Calculate rects
                Rect minRect = new Rect(position.x, position.y, FieldWidth, position.height);
                Rect sliderRect = new Rect(position.x + (FieldWidth + SliderPadding), position.y, position.width - (FieldWidth + SliderPadding) * 2, position.height);
                Rect maxRect = new Rect(position.x + position.width - FieldWidth, position.y, FieldWidth, position.height);

                EditorGUI.BeginChangeCheck();

                min = Mathf.Clamp(EditorGUI.IntField(minRect, (int)min), RangeIntLimit.minLimit, RangeIntLimit.maxLimit);
                EditorGUI.MinMaxSlider(sliderRect, ref min, ref max, RangeIntLimit.minLimit, RangeIntLimit.maxLimit);
                max = Mathf.Clamp(EditorGUI.IntField(maxRect, (int)max), RangeIntLimit.minLimit, RangeIntLimit.maxLimit);

                if (EditorGUI.EndChangeCheck())
                {
                    minValue.intValue = (int)min;
                    maxValue.intValue = (int)max;
                }
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Use RangeIntLimit with RangeInt.");
            }
        }
    }

    [CustomPropertyDrawer(typeof(RangeFloatLimitAttribute))]
    public class RangeFloatLimitDrawer : PropertyDrawer
    {
        private const float FieldWidth = 30f;
        private const float SliderPadding = 5f;

        private RangeFloatLimitAttribute RangeFloatLimit
        {
            get
            {
                return attribute as RangeFloatLimitAttribute;
            }
        }

        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.type == typeof(RangeFloat).Name)
            {
                SerializedProperty minValue = property.FindPropertyRelative("minValue");
                SerializedProperty maxValue = property.FindPropertyRelative("maxValue");

                float min = minValue.floatValue;
                float max = maxValue.floatValue;

                position = EditorGUI.PrefixLabel(position, label);

                // Calculate rects
                Rect minRect = new Rect(position.x, position.y, FieldWidth, position.height);
                Rect sliderRect = new Rect(position.x + (FieldWidth + SliderPadding), position.y, position.width - (FieldWidth + SliderPadding) * 2, position.height);
                Rect maxRect = new Rect(position.x + position.width - FieldWidth, position.y, FieldWidth, position.height);

                EditorGUI.BeginChangeCheck();

                min = Mathf.Clamp(EditorGUI.FloatField(minRect, min), RangeFloatLimit.minLimit, RangeFloatLimit.maxLimit);
                EditorGUI.MinMaxSlider(sliderRect, ref min, ref max, RangeFloatLimit.minLimit, RangeFloatLimit.maxLimit);
                max = Mathf.Clamp(EditorGUI.FloatField(maxRect, max), RangeFloatLimit.minLimit, RangeFloatLimit.maxLimit);

                if (EditorGUI.EndChangeCheck())
                {
                    minValue.floatValue = min;
                    maxValue.floatValue = max;
                }
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Use RangeFloatLimit with RangeFloat.");
            }
        }
    }
}
