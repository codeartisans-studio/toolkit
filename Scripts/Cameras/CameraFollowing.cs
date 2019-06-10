using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit.Cameras
{
    [AddComponentMenu("Toolkit/Cameras/CameraFollowing")]
    public class CameraFollowing : MonoBehaviour
    {
        public Transform target;
        public float smoothing = 5f;

        private Vector3 offset;

        // Use this for initialization
        void Start()
        {
            offset = transform.position - target.position;
        }

        void LateUpdate()
        {
            Vector3 targetCamPos = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
    }
}
