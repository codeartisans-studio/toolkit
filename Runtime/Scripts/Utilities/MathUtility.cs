using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Toolkit
{
    public static class MathUtility
    {
        /// <summary>
        /// Transforms the position from local space to world space. This is similar to Transform.TransformPoint but does not require a Transform.
        /// </summary>
        /// <param name="worldPosition">The world position of the object.</param>
        /// <param name="rotation">The world rotation of the object.</param>
        /// <param name="localPosition">The local position of the object</param>
        /// <returns>The world space position.</returns>
        public static Vector3 TransformPoint(Vector3 worldPosition, Quaternion rotation, Vector3 localPosition)
        {
            return worldPosition + (rotation * localPosition);
        }

        /// <summary>
        /// Transforms the position from world space to local space. This is similar to Transform.InverseTransformPoint but does not require a Transform.
        /// </summary>
        /// <param name="worldPosition">The world position of the object.</param>
        /// <param name="rotation">The world rotation of the object.</param>
        /// <param name="position">The position of the object.</param>
        /// <returns>The local space position.</returns>
        public static Vector3 InverseTransformPoint(Vector3 worldPosition, Quaternion rotation, Vector3 position)
        {
            var diff = position - worldPosition;
            return Quaternion.Inverse(rotation) * diff;
        }

        // 一定数量元素随机放入数组
        // 如果需要数量大于数组数量，重新生成数组再次随机
        // 返回一个一定数量索引素组
        public static int[] RandomIndices(int elementCount, int arrayCount)
        {
            List<int> indices = new List<int>();

            int[] randomIndices = new int[elementCount];

            for (int i = 0; i < elementCount; i++)
            {
                if (indices.Count == 0)
                    indices.AddRange(Enumerable.Range(0, arrayCount));

                int index = indices[Random.Range(0, indices.Count)];
                randomIndices[i] = index;
                indices.Remove(index);
            }

            return randomIndices;
        }

        // 一定数量元素随机放入数组
        // 如果需要数量大于数组数量，重新生成数组再次随机
        // 返回数组中对应的元素数量
        public static int[] RandomCount(int elementCount, int arrayCount)
        {
            List<int> indices = new List<int>();

            int[] randomCount = new int[arrayCount];

            for (int i = 0; i < elementCount; i++)
            {
                if (indices.Count == 0)
                    indices.AddRange(Enumerable.Range(0, arrayCount));

                int index = indices[Random.Range(0, indices.Count)];
                randomCount[index] += 1;
                indices.Remove(index);
            }

            return randomCount;
        }
    }
}