using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit
{
    [AddComponentMenu("Toolkit/Transforms/Floating")]
    public class Floating : MonoBehaviour
    {
        public float floatSpeed = 1f;
        public float floatRange = 1f;
        public float rotationSpeed = 20.0f;

        // Update is called once per frame
        void Update()
        {
            Vector3 localPosition = transform.localPosition;
            Vector3 eulerAngles = transform.eulerAngles;

            localPosition.y = Mathf.Sin(Time.time * floatSpeed) * floatRange;
            eulerAngles.y += rotationSpeed * Time.deltaTime;

            transform.localPosition = localPosition;
            transform.eulerAngles = eulerAngles;
        }
    }
}
