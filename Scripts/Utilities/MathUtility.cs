using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit.Utilities
{
	public static class MathUtility
	{
		// Cubic Hermite spline (Unit interval (0, 1))
		public static float HermiteLerp (float p0, float p1, float m0, float m1, float t)
		{
			// h_00(t) = 2t^3-3t^2+1
			// h_10(t) = t^3-2t^2+t
			// h_01(t) = -2t^3+3t^2
			// h_11(t) = t^3-t^2

			float t2 = t * t;
			float t3 = t * t2;

			return (2 * t3 - 3 * t2 + 1) * p0 + (t3 - 2 * t2 + t) * m0 + (-2 * t3 + 3 * t2) * p1 + (t3 - t2) * m1;
		}

		// Cubic Hermite spline (Interpolation on an arbitrary interval)
		public static float HermiteLerp (float x0, float y0, float x1, float y1, float outTangent, float inTangent, float x)
		{
			float dt = (x1 - x0);

			float t = (x - x0) / dt;

			float m0 = outTangent * dt;
			float m1 = inTangent * dt;

			return HermiteLerp (y0, y1, m0, m1, t);
		}

		public static float HermiteLerp (Vector2 start, Vector2 end, float outTangent, float inTangent, float x)
		{
			return HermiteLerp (start.x, start.y, end.x, end.y, outTangent, inTangent, x);
		}
	}
}
