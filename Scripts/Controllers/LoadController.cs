//This script lets you load a Scene asynchronously. It uses an asyncOperation to calculate the progress and outputs the current progress to Text (could also be used to make progress bars).

//Attach this script to a GameObject
//Create a Button (Create>UI>Button) and a Text GameObject (Create>UI>Text) and attach them both to the Inspector of your GameObject
//In Play Mode, press your Button to load the Scene, and the Text changes depending on progress. Press the space key to activate the Scene.
//Note: The progress may look like it goes straight to 100% if your Scene doesn’t have a lot to load.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Toolkit.Controllers
{
    [AddComponentMenu("Toolkit/Controllers/LoadController")]
    public class LoadController : Singleton<LoadController>
    {
        public bool autoActivation = false;
        public float progressStep = 1f;
        [NonSerialized]
        public float showProgress;

        private AsyncOperation asyncOperation;

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            yield return null;

            float toProgress = 0;
            showProgress = 0;

            //Begin to load the Scene you specify
            asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            //Don't let the Scene activate until you allow it to
            asyncOperation.allowSceneActivation = false;

            //When the load is still in progress, output the Text and progress bar
            while (!asyncOperation.isDone)
            {
                toProgress = asyncOperation.progress * 100;
                // Debug.Log("Loading progress: " + asyncOperation.progress);

                while (showProgress < toProgress)
                {
                    showProgress += progressStep;
                    Debug.Log("Loading progress: " + showProgress);

                    yield return null;
                }

                // Check if the load has finished
                if (asyncOperation.progress >= 0.9f)
                {
                    toProgress = 100;

                    while (showProgress < toProgress)
                    {
                        showProgress += progressStep;
                        Debug.Log("Loading progress: " + showProgress);

                        yield return null;
                    }

                    if (autoActivation)
                    {
                        //Activate the Scene
                        asyncOperation.allowSceneActivation = true;
                    }
                }

                yield return null;
            }
        }

        public void LoadScene(string sceneName)
        {
            //Start loading the Scene asynchronously and output the progress bar
            StartCoroutine(LoadSceneAsync(sceneName));
        }

        public void AlowSceneActivation(bool allowSceneActivation)
        {
            asyncOperation.allowSceneActivation = allowSceneActivation;
        }
    }
}