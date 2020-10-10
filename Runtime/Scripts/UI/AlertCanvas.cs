using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Toolkit
{
    [RequireComponent(typeof(Canvas))]
    public abstract class AlertCanvas<T> : SingletonWindow<T> where T : AlertCanvas<T>
    {
        public Text messageText;
        public Button[] confirmButtons;
        public Button[] cancelButtons;

        public Text confirmText;
        public Text cancelText;

        private Action confirmAction;
        private Action cancelAction;

        // Start is called before the first frame update
        void Start()
        {
            foreach (Button confirmButton in confirmButtons)
            {
                confirmButton.onClick.AddListener(() =>
                {
                    Close();

                    if (confirmAction != null)
                        confirmAction();
                });
            }

            foreach (Button cancelButton in cancelButtons)
            {
                cancelButton.onClick.AddListener(() =>
                {
                    Close();

                    if (cancelAction != null)
                        cancelAction();
                });
            }
        }

        public void Open(string message, Action confirmAction, Action cancelAction = null)
        {
            Open(message, "确认", confirmAction, "取消", cancelAction);
        }

        public void Open(string message, string confirmText, Action confirmAction, string cancelText = null, Action cancelAction = null)
        {
            Open(false);

            messageText.text = message;

            this.confirmText.text = confirmText;
            this.confirmAction = confirmAction;
            if (cancelText != null)
                this.cancelText.text = cancelText;
            if (cancelAction != null)
                this.cancelAction = cancelAction;
        }
    }
}
