using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit.UI
{
    [AddComponentMenu("Toolkit/UI/CanvasVisibility")]
    [RequireComponent(typeof(Canvas))]
    public class CanvasVisibility : MonoBehaviour
    {
        public GameObject board;

        private bool showing;
        private Action action;

        public void Show(Action callback)
        {
            Show();

            showing = true;

            action = callback;
        }

        public void Show()
        {
            board.SetActive(true);
        }

        public void Hide()
        {
            board.SetActive(false);
        }

        public void Back()
        {
            if (showing)
            {
                showing = false;

                action();
            }
        }
    }
}
