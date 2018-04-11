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

		public Range (T minValue, T maxValue)
		{
			this.minValue = minValue;
			this.maxValue = maxValue;
		}
	}
}
