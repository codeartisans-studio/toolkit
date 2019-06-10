using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit.Meshs
{
    [AddComponentMenu("Toolkit/Meshs/OffsetMovement")]
    public class OffsetMovement : MonoBehaviour
    {
        public Vector2 speed = Vector2.left;

        private MeshRenderer meshRenderer;

        void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 offset = -speed * Time.time;
            meshRenderer.material.SetTextureOffset("_MainTex", offset);
        }
    }
}
