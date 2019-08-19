using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Toolkit
{
    [AddComponentMenu("Toolkit/Localization/LocalizedText")]
    public class LocalizedText : MonoBehaviour
    {
        public string key;

        // Use this for initialization
        void Start()
        {
            Text text = GetComponent<Text>();
            text.text = LocalizationController.Instance().GetLocalizedValue(key);
        }
    }
}