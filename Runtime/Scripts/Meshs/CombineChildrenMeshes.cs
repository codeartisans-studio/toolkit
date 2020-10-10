using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit
{
    public class CombineChildrenMeshes : MonoBehaviour
    {
        public Material material;

        // Start is called before the first frame update
        void Start()
        {
            // 合并所有子物体Mesh
            List<GameObject> combineObjects = new List<GameObject>();

            foreach (Transform child in transform)
                combineObjects.Add(child.gameObject);

            // Start中处理，保证更改顶点色材质在Awake中先执行
            Mesh mesh = MeshUtility.CombineMeshes(gameObject.transform, combineObjects.ToArray());
            gameObject.AddComponent<MeshFilter>().sharedMesh = mesh;
            gameObject.AddComponent<MeshRenderer>().sharedMaterial = material;
        }
    }
}
