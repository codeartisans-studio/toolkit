﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit
{
    [AddComponentMenu("Toolkit/Meshs/Offset Movement")]
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
