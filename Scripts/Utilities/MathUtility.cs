using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Toolkit
{
    public static class MathUtility
    {
        // 从数组中随机出一定数量的索引
        public static int[] RandomIndices(int count, int arrayCount)
        {
            List<int> indices = Enumerable.Range(0, arrayCount).ToList();

            // 如果随机数量大于数组数量，直接返回全部数组
            if (count >= arrayCount)
                return indices.ToArray();

            int[] randomIndices = new int[count];

            for (int i = 0; i < count; i++)
            {
                randomIndices[i] = indices[Random.Range(0, indices.Count)];
                indices.Remove(randomIndices[i]);
            }

            return randomIndices;
        }
    }
}