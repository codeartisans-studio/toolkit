﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Toolkit
{
    [AddComponentMenu("Toolkit/Settings/Additive Scenes Setting")]
    public class AdditiveScenesSetting : MonoBehaviour
    {
        public string[] scenes;

        void Awake()
        {
            for (int i = 0; i < scenes.Length; i++)
            {
                SceneManager.LoadScene(scenes[i], LoadSceneMode.Additive);
            }
        }
    }
}
