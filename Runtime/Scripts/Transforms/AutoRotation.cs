﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit
{
    [AddComponentMenu("Toolkit/Transforms/Auto Rotation")]
    public class AutoRotation : MonoBehaviour
    {
        public Vector3 speed = Vector3.forward;
        public Space space = Space.Self;

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(speed * Time.deltaTime, space);
        }
    }
}
