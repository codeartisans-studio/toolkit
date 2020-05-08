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
        public Button cancelButton;
        public Button confirmButton;

        public Action submitAction;
        public Action cancelAction;

        // Start is called before the first frame update
        void Start()
        {
            confirmButton.onClick.AddListener(() =>
            {
                Close();

                submitAction();
            });

            cancelButton.onClick.AddListener(() =>
            {
                Close();

                cancelAction();
            });
        }

        public void Open(string message, Action confirmAction, Action cancelAction = null)
        {
            Open(false);

            messageText.text = message;

            this.submitAction = confirmAction;
            this.cancelAction = cancelAction;
        }
    }
}
