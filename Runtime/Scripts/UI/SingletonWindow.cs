using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit
{
    [RequireComponent(typeof(Canvas))]
    public class SingletonWindow<T> : Singleton<T> where T : SingletonWindow<T>
    {
        public GameObject board;

        public bool IsOpened { get => board.activeSelf; }

        protected virtual void Awake()
        {
            Close();
        }

        protected virtual void OnEnable()
        {
            WindowsManager.Add(board);
        }

        protected virtual void OnDisable()
        {
            WindowsManager.Remove(board);
        }

        public virtual void Open(bool isCloseOthers = true)
        {
            if (isCloseOthers)
                WindowsManager.CloseAll();

            WindowsManager.Open(board);
        }

        public virtual void Close()
        {
            WindowsManager.Close(board);
        }
    }
}