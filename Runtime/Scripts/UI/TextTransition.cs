using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Toolkit
{
    public class TextTransition : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        public Text text;
        public Color normalColor;
        public Color highlightedColor;
        public Color pressedColor;

        public void OnPointerEnter(PointerEventData eventData) => text.color = highlightedColor;
        public void OnPointerExit(PointerEventData eventData) => text.color = normalColor;
        public void OnPointerDown(PointerEventData eventData) => text.color = pressedColor;
        public void OnPointerUp(PointerEventData eventData) => text.color = highlightedColor;
    }
}