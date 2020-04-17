using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Toolkit
{
    public static class MathUtility
    {
        public static int[] RandomIndices(int count, int arrayCount)
        {
            Debug.AssertFormat(count < arrayCount, "Random count {0} greater than array count {1}!", count, arrayCount);

            List<int> indices = Enumerable.Range(0, arrayCount).ToList();

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