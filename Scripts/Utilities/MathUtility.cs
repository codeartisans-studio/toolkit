using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Toolkit
{
    public static class MathUtility
    {
        // 从数组中随机出一定数量的索引，如果需要数量大于数组数量，重新生成数组再次随机
        public static int[] RandomIndices(int count, int arrayCount)
        {
            List<int> indices = Enumerable.Range(0, arrayCount).ToList();

            int[] randomIndices = new int[count];

            for (int i = 0; i < count; i++)
            {
                randomIndices[i] = indices[Random.Range(0, indices.Count)];
                indices.Remove(randomIndices[i]);

                if (indices.Count == 0)
                    indices.AddRange(Enumerable.Range(0, arrayCount));
            }

            return randomIndices;
        }
    }
}