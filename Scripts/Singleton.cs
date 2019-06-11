﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T instance;

        public static T Instance()
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                Debug.AssertFormat(instance, "There needs to be one active [{0}] script on a GameObject in your scene.", typeof(T));
            }

            return instance;
        }
    }
}
