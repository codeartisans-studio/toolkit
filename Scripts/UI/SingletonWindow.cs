using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Toolkit.Managers;

namespace Toolkit.UI
{
    [RequireComponent(typeof(Canvas))]
    public class SingletonWindow<T> : Singleton<T> where T : SingletonWindow<T>
    {
        public GameObject board;

        protected virtual void Awake()
        {
            Close();
        }

        protected virtual void OnEnable()
        {
            WindowsManager.Add(board);
        }

        protected virtual void OnDestroy()
        {
            WindowsManager.Remove(board);
        }

        public void Open(bool isCloseOthers = true)
        {
            if (isCloseOthers)
                WindowsManager.CloseAll();

            WindowsManager.Open(board);
        }

        public void Close()
        {
            WindowsManager.Close(board);
        }
    }
}