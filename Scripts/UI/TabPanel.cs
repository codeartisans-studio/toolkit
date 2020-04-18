using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Toolkit
{
    [Serializable]
    public class Tab
    {
        public Toggle toggle;
        public GameObject[] showWhenActive;
        public GameObject[] hideWhenActive;
        public GameObject[] scaleWhenActive;
    }

    public class TabPanel : MonoBehaviour
    {
        public Tab[] tabs;

        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < tabs.Length; i++)
            {
                int index = i;

                foreach (GameObject obj in tabs[index].showWhenActive)
                    obj.SetActive(tabs[index].toggle.isOn);
                foreach (GameObject obj in tabs[index].hideWhenActive)
                    obj.SetActive(!tabs[index].toggle.isOn);
                foreach (GameObject obj in tabs[index].scaleWhenActive)
                    obj.transform.localScale = tabs[index].toggle.isOn ? Vector3.one : Vector3.zero;

                tabs[index].toggle.onValueChanged.AddListener((bool value) =>
                {
                    foreach (GameObject obj in tabs[index].showWhenActive)
                        obj.SetActive(value);
                    foreach (GameObject obj in tabs[index].hideWhenActive)
                        obj.SetActive(!value);
                    foreach (GameObject obj in tabs[index].scaleWhenActive)
                        obj.transform.localScale = value ? Vector3.one : Vector3.zero;
                });
            }
        }
    }
}