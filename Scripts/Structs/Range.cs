using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit.Structs
{
    [Serializable]
    public struct Range<T>
    {
        public T minValue;
        public T maxValue;

        public Range(T minValue, T maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }
    }

    [Serializable]
    public struct RangeInt
    {
        public int minValue;
        public int maxValue;

        public RangeInt(int minValue, int maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        public int Random()
        {
            return UnityEngine.Random.Range(minValue, maxValue);
        }
    }

    [Serializable]
    public struct RangeFloat
    {
        public float minValue;
        public float maxValue;

        public RangeFloat(float minValue, float maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        public float Random()
        {
            return UnityEngine.Random.Range(minValue, maxValue);
        }
    }
}
