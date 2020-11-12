using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit
{
    [DisallowMultipleComponent]
    public class DontDestroy : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
