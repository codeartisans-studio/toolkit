using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit
{
    [AddComponentMenu("Toolkit/Cameras/Multi Display")]
    public class MultiDisplay : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            for (int i = 0; i < Display.displays.Length; i++)
            {
                Display.displays[i].Activate();
            }
        }
    }
}
