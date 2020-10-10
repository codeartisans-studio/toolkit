using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Toolkit;

public class CombineObjectsMeshes : MonoBehaviour
{
    public GameObject[] combineObjects;

    public Material material;

    // Start is called before the first frame update
    void Start()
    {
        // Start中处理，保证更改顶点色材质在Awake中先执行
        Mesh mesh = MeshUtility.CombineMeshes(gameObject.transform, combineObjects);
        gameObject.AddComponent<MeshFilter>().sharedMesh = mesh;
        gameObject.AddComponent<MeshRenderer>().sharedMaterial = material;
    }
}
