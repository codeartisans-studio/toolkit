using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit
{
    public class Material2VertexColor : MonoBehaviour
    {
        public Material colorMaterial;
        public string colorProperty = "_Color";

        // Start is called before the first frame update
        void Awake()
        {
            Renderer[] renderers = GetComponentsInChildren<Renderer>();

            // 遍历MeshRenderer
            foreach (Renderer renderer in renderers)
            {
                Mesh mesh = null;

                if (renderer is MeshRenderer)
                {
                    MeshFilter filter = renderer.GetComponent<MeshFilter>();
                    // 会实例化一个新Mesh
                    mesh = filter.mesh;
                }
                else if (renderer is SkinnedMeshRenderer)
                {
                    // 有蒙皮信息，需要复制骨骼权重等额外信息
                    SkinnedMeshRenderer skinnedMeshRenderer = renderer as SkinnedMeshRenderer;
                    // 实例化新Mesh
                    mesh = new Mesh();
                    // 复制所需信息
                    mesh.vertices = skinnedMeshRenderer.sharedMesh.vertices;
                    mesh.triangles = skinnedMeshRenderer.sharedMesh.triangles;
                    mesh.bindposes = skinnedMeshRenderer.sharedMesh.bindposes;
                    mesh.boneWeights = skinnedMeshRenderer.sharedMesh.boneWeights;
                    // 设置新Mesh
                    skinnedMeshRenderer.sharedMesh = mesh;
                }
                else
                {
                    // 忽略其他Renderer
                    // 如ParticleSystemRenderer
                    continue;
                }

                // 获得分离公共顶点的Mesh
                mesh = MeshUtility.SplitSubMesh(mesh);

                // 三角面列表
                List<int> triangles = new List<int>();
                // 顶点色数组
                Color[] colors = new Color[mesh.vertexCount];

                // 材质数组
                Material[] materials = renderer.materials;

                // 遍历子mesh
                for (int i = 0; i < mesh.subMeshCount; i++)
                {
                    // 构建统一三角面，合并子Mesh
                    triangles.AddRange(mesh.GetTriangles(i));

                    // 获得当前子Mesh的材质文件，材质数组小于子Mesh数量时，取最后一个材质
                    Material material = materials[Mathf.Min(i, materials.Length - 1)];

                    // 获得材质颜色属性
                    Color color = material.GetColor(colorProperty);

                    // 存储顶点颜色
                    foreach (int index in mesh.GetIndices(i))
                        colors[index] = color;
                }

                // 合并Mesh
                mesh.triangles = triangles.ToArray();
                // 设置顶点色
                mesh.colors = colors;

                mesh.RecalculateNormals();

                // 设置统一材质
                renderer.materials = new Material[] { colorMaterial };
            }
        }
    }
}
