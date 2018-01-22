using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit.Structs
{
	[Serializable]
	public struct Range
	{
		public float minValue;
		public float maxValue;

		public Range (float minValue, float maxValue)
		{
			this.minValue = minValue;
			this.maxValue = maxValue;
		}
	}
}
