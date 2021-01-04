using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit
{
    [AddComponentMenu("Toolkit/Transforms/Follow Target")]
    public class FollowTarget : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset;

        private void LateUpdate()
        {
            transform.position = target.position + offset;
        }
    }
}
