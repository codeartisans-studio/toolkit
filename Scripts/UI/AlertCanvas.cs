using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Toolkit
{
    [AddComponentMenu("Toolkit/UI/AlertCanvas")]
    [RequireComponent(typeof(Canvas))]
    public class AlertCanvas : SingletonWindow<AlertCanvas>
    {
        public Text messageText;
        public Button[] confirmButtons;
        public Button[] cancelButtons;

        public Action confirmAction;
        public Action cancelAction;

        // Start is called before the first frame update
        void Start()
        {
            foreach (Button confirmButton in confirmButtons)
            {
                confirmButton.onClick.AddListener(() =>
                {
                    Close();

                    confirmAction();
                });
            }

            foreach (Button cancelButton in cancelButtons)
            {
                cancelButton.onClick.AddListener(() =>
                {
                    Close();

                    cancelAction();
                });
            }
        }

        public void Open(string message, Action confirmAction, Action cancelAction = null)
        {
            Open(false);

            messageText.text = message;

            this.confirmAction = confirmAction;
            this.cancelAction = cancelAction;
        }
    }
}
