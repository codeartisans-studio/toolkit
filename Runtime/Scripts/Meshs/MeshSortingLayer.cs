using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit
{
    [AddComponentMenu("Toolkit/Meshs/Mesh Sorting Layer")]
    public class MeshSortingLayer : MonoBehaviour
    {
        public string sortingLayer = "Default";
        public int sortingOrder = 0;

        void Awake()
        {
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.sortingLayerName = sortingLayer;
            meshRenderer.sortingOrder = sortingOrder;
        }
    }
}
