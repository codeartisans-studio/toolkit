using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Toolkit
{
    [AddComponentMenu("Toolkit/Controllers/StartupController")]
    public class StartupController : Singleton<StartupController>
    {
        void Awake()
        {
            LocalizationController.Instance().LoadLocalizedText("en.json");
            // LocalizationController.Instance().LoadLocalizedText("zh-Hans.json");
            // LocalizationController.Instance().LoadLocalizedText("zh-Hant.json");
        }

        // Use this for initialization
        private IEnumerator Start()
        {
            while (!LocalizationController.Instance().GetIsReady())
            {
                yield return null;
            }

            SceneManager.LoadScene("Main");
        }
    }
}