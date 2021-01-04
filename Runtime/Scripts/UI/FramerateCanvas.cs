using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Toolkit
{
    // An FPS counter.
    // It calculates frames/second over each updateInterval,
    // so the display does not keep changing wildly.
    [AddComponentMenu("Toolkit/UI/Framerate Canvas")]
    [RequireComponent(typeof(Canvas))]
    public class FramerateCanvas : MonoBehaviour
    {
        public Text fpsText;

        public float updateInterval = 0.5f;
        private double lastInterval;
        private int frames = 0;
        private float fps;

        void Start()
        {
            lastInterval = Time.realtimeSinceStartup;
            frames = 0;
        }

        void Update()
        {
            ++frames;
            float timeNow = Time.realtimeSinceStartup;
            if (timeNow > lastInterval + updateInterval)
            {
                fps = (float)(frames / (timeNow - lastInterval));
                frames = 0;
                lastInterval = timeNow;

                fpsText.text = string.Format("{0:F2} FPS", fps);
            }
        }
    }
}
