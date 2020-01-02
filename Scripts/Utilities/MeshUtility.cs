using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Toolkit
{
    public static class MeshUtility
    {
        // 炸开不同id公共顶点
        public static Mesh SplitSubMesh(Mesh mesh)
        {
            // 顶点列表
            List<Vector3> vertices = new List<Vector3>(mesh.vertices);

            // 子mesh顶点信息
            List<int>[] subMeshTriangles = new List<int>[mesh.subMeshCount];

            // 遍历子mesh
            for (int i = 0; i < mesh.subMeshCount; i++)
            {
                // 初始化子mesh顶点
                subMeshTriangles[i] = new List<int>(mesh.GetTriangles(i));

                // 子mesh公共顶点信息
                HashSet<int> commonTriangles = new HashSet<int>();

                // 顶点替换map
                Dictionary<int, int> trianglesMapping = new Dictionary<int, int>();

                // 遍历之前子mesh
                for (int j = 0; j < i; j++)
                {
                    // 查找重复顶点
                    commonTriangles.UnionWith(subMeshTriangles[i].Intersect(subMeshTriangles[j]));
                }

                // 遍历重复顶点
                foreach (int index in commonTriangles)
                {
                    // 添加新顶点
                    vertices.Add(mesh.vertices[index]);
                    // 构建替换顶点数据
                    trianglesMapping[index] = vertices.Count - 1;

                    // Debug.Log("replace: " + index + " with: " + replace[index]);
                }

                // 遍历子mesh索引
                for (int j = 0; j < subMeshTriangles[i].Count; j++)
                {
                    // 机路原索引
                    int index = subMeshTriangles[i][j];

                    // 如果需替换
                    if (trianglesMapping.ContainsKey(index))
                        // 替换索引
                        subMeshTriangles[i][j] = trianglesMapping[index];
                }


                // 构建三角面信息
                // triangles.AddRange(subMeshTriangles[i]);
            }

            mesh.vertices = vertices.ToArray();

            // 遍历子mesh
            for (int i = 0; i < mesh.subMeshCount; i++)
            {
                mesh.SetTriangles(subMeshTriangles[i].ToArray(), i);
            }

            mesh.RecalculateNormals();

            return mesh;
        }
    }
}