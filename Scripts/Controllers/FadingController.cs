using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Toolkit
{
    [AddComponentMenu("Toolkit/Controllers/FadingController")]
    [RequireComponent(typeof(Animator))]
    public class FadingController : Singleton<FadingController>
    {
        private Animator anim;
        private Action action;

        void Awake()
        {
            anim = GetComponent<Animator>();
        }

        void Start()
        {
            FadeIn();
        }

        public void FadeIn(Action callback = null)
        {
            anim.SetTrigger("FadeIn");

            if (callback != null)
            {
                action = callback;
            }
        }

        public void FadeOut(Action callback = null)
        {
            anim.SetTrigger("FadeOut");

            if (callback != null)
            {
                action = callback;
            }
        }

        // animation clip callback
        public void Done()
        {
            if (action != null)
            {
                action();

                action = null;
            }
        }

        public void LoadScene(int buildIndex, LoadSceneMode mode = LoadSceneMode.Single)
        {
            FadeOut(() =>
            {
                SceneManager.LoadScene(buildIndex, mode);
            });
        }

        public void LoadScene(string scene, LoadSceneMode mode = LoadSceneMode.Single)
        {
            FadeOut(() =>
            {
                SceneManager.LoadScene(scene, mode);
            });
        }
    }
}
