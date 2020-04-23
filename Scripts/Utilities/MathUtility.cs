using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Toolkit
{
    public static class MathUtility
    {
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