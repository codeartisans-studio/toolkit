using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit.Attributes
{
	public class RangeLimitAttribute : PropertyAttribute
	{
		public float minLimit;
		public float maxLimit;

		public RangeLimitAttribute (float minLimit, float maxLimit)
		{
			this.minLimit = minLimit;
			this.maxLimit = maxLimit;
		}
	}
}
