using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Toolkit.Attributes;

namespace Toolkit.Editor
{
	[CustomPropertyDrawer (typeof(RangeLimitAttribute))]
	public class RangeLimitDrawer : PropertyDrawer
	{
		private const float FieldWidth = 30f;
		private const float SliderMargin = 5f;

		// Draw the property inside the given rect
		public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
		{
			// First get the attribute since it contains the range for the slider
			RangeLimitAttribute range = attribute as RangeLimitAttribute;

			if (property.type == "Range") {
				SerializedProperty minValue = property.FindPropertyRelative ("minValue");
				SerializedProperty maxValue = property.FindPropertyRelative ("maxValue");

				float min = minValue.floatValue;
				float max = maxValue.floatValue;

				position = EditorGUI.PrefixLabel (position, label);

				// Calculate rects
				Rect minRect = new Rect (position.x, position.y, FieldWidth, position.height);
				Rect sliderRect = new Rect (position.x + (FieldWidth + SliderMargin), position.y, position.width - (FieldWidth + SliderMargin) * 2, position.height);
				Rect maxRect = new Rect (position.x + position.width - FieldWidth, position.y, FieldWidth, position.height);

				EditorGUI.BeginChangeCheck ();

				min = Mathf.Clamp (EditorGUI.FloatField (minRect, min), range.minLimit, range.maxLimit);
				EditorGUI.MinMaxSlider (sliderRect, ref min, ref max, range.minLimit, range.maxLimit);
				max = Mathf.Clamp (EditorGUI.FloatField (maxRect, max), range.minLimit, range.maxLimit);

				if (EditorGUI.EndChangeCheck ()) {
					minValue.floatValue = min;
					maxValue.floatValue = max;
				}
			} else {
				EditorGUI.LabelField (position, label.text, "Use RangeLimit with Range.");
			}
		}
	}
}
