using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Toolkit.Attributes;

namespace Toolkit.Editor
{
    // This defines how the ColorSpacer should be drawn
    // in the inspector, when inspecting a GameObject with
    // a MonoBehaviour which uses the ColorSpacer attribute
    [CustomPropertyDrawer(typeof(ColorSpacerAttribute))]
    public class ColorSpacerDrawer : DecoratorDrawer
    {
        private ColorSpacerAttribute ColorSpacer
        {
            get
            {
                return attribute as ColorSpacerAttribute;
            }
        }

        public override float GetHeight()
        {
            return base.GetHeight() + ColorSpacer.spaceHeight;
        }

        public override void OnGUI(Rect position)
        {
            // calculate the rect values for where to draw the line in the inspector
            float lineX = (position.x + (position.width / 2)) - ColorSpacer.lineWidth / 2;
            float lineY = position.y + (ColorSpacer.spaceHeight / 2);
            float lineWidth = ColorSpacer.lineWidth;
            float lineHeight = ColorSpacer.lineHeight;

            // Draw the line in the calculated place in the inspector
            // (using the built in white pixel texture, tinted with GUI.color)
            Color oldGuiColor = GUI.color;
            GUI.color = ColorSpacer.lineColor;
            EditorGUI.DrawPreviewTexture(new Rect(lineX, lineY, lineWidth, lineHeight), Texture2D.whiteTexture);
            GUI.color = oldGuiColor;
        }
    }
}
