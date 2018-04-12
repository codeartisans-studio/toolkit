using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Toolkit.Attributes;
using Toolkit.Structs;

namespace Toolkit.Editor
{
	[CustomPropertyDrawer (typeof(RangeLimitAttribute))]
	public class RangeLimitDrawer : PropertyDrawer
	{
		private const float FieldWidth = 30f;
		private const float SliderPadding = 5f;

		private RangeLimitAttribute RangeLimit {
			get {
				return attribute as RangeLimitAttribute;
			}
		}

		// Draw the property inside the given rect
		public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
		{
			if (property.type == typeof(RangeFloat).Name) {
				SerializedProperty minValue = property.FindPropertyRelative ("minValue");
				SerializedProperty maxValue = property.FindPropertyRelative ("maxValue");

				float min = minValue.floatValue;
				float max = maxValue.floatValue;

				position = EditorGUI.PrefixLabel (position, label);

				// Calculate rects
				Rect minRect = new Rect (position.x, position.y, FieldWidth, position.height);
				Rect sliderRect = new Rect (position.x + (FieldWidth + SliderPadding), position.y, position.width - (FieldWidth + SliderPadding) * 2, position.height);
				Rect maxRect = new Rect (position.x + position.width - FieldWidth, position.y, FieldWidth, position.height);

				EditorGUI.BeginChangeCheck ();

				min = Mathf.Clamp (EditorGUI.FloatField (minRect, min), RangeLimit.minLimit, RangeLimit.maxLimit);
				EditorGUI.MinMaxSlider (sliderRect, ref min, ref max, RangeLimit.minLimit, RangeLimit.maxLimit);
				max = Mathf.Clamp (EditorGUI.FloatField (maxRect, max), RangeLimit.minLimit, RangeLimit.maxLimit);

				if (EditorGUI.EndChangeCheck ()) {
					minValue.floatValue = min;
					maxValue.floatValue = max;
				}
			} else {
				EditorGUI.LabelField (position, label.text, "Use RangeLimit with RangeFloat.");
			}
		}
	}
}
