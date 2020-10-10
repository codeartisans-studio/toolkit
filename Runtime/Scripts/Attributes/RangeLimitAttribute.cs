using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit
{
    public class RangeIntLimitAttribute : PropertyAttribute
    {
        public int minLimit;
        public int maxLimit;

        public RangeIntLimitAttribute(int minLimit, int maxLimit)
        {
            this.minLimit = minLimit;
            this.maxLimit = maxLimit;
        }
    }

    public class RangeFloatLimitAttribute : PropertyAttribute
    {
        public float minLimit;
        public float maxLimit;

        public RangeFloatLimitAttribute(float minLimit, float maxLimit)
        {
            this.minLimit = minLimit;
            this.maxLimit = maxLimit;
        }
    }
}
