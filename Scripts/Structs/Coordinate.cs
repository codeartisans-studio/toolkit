using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit.Structs
{
	[Serializable]
	public struct Coordinate
	{
		public int x;
		public int y;

		public Coordinate (int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	}
}
