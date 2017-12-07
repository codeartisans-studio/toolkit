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
			float dx = (x1 - x0);

			float t = (x - x0) / dx;

			float m0 = outTangent * dx;
			float m1 = inTangent * dx;

			return HermiteLerp (y0, y1, m0, m1, t);
		}

		public static float HermiteLerp (Vector2 p0, Vector2 p1, float outTangent, float inTangent, float x)
		{
			return HermiteLerp (p0.x, p0.y, p1.x, p1.y, outTangent, inTangent, x);
		}

		// Catmull–Rom spline
		public static float CatmullRomLerp (float x0, float y0, float x1, float y1, float x2, float y2, float x3, float y3, float x)
		{
			// m_k = (p_k+1-p_k-1) / (t_k+1 - t_k-1)

			float m1 = (y2 - y0) / (x2 - x0);
			float m2 = (y3 - y1) / (x3 - x1);

			return HermiteLerp (x0, y0, y1, y2, m1, m2, x);
		}

		public static float CatmullRomLerp (Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float x)
		{
			return CatmullRomLerp (p0.x, p0.y, p1.x, p1.y, p2.x, p2.y, p3.x, p3.y, x);
		}

		// Cardinal spline
		public static float CardinalLerp (float x0, float y0, float x1, float y1, float x2, float y2, float x3, float y3, float continuity, float x)
		{
			continuity = Mathf.Clamp (continuity, -1, 1);

			// m_k = (1-c) * (p_k+1-p_k-1) / (t_k+1 - t_k-1)

			float m1 = (1 - continuity) * (y2 - y0) / (x2 - x0);
			float m2 = (1 - continuity) * (y3 - y1) / (x3 - x1);

			return HermiteLerp (x0, y0, y1, y2, m1, m2, x);
		}

		public static float CardinalLerp (Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float continuity, float x)
		{
			return CardinalLerp (p0.x, p0.y, p1.x, p1.y, p2.x, p2.y, p3.x, p3.y, continuity, x);
		}

		// Kochanek–Bartels spline
		// t	tension		Changes the length of the tangent vector
		// b	bias		Primarily changes the direction of the tangent vector
		// c	continuity	Changes the sharpness in change between tangents
		// Tension		T = +1→ Tight				T = −1→ Round
		// Bias			B = +1→ Post Shoot			B = −1→ Pre shoot
		// Continuity	C = +1→ Inverted corners	C = −1→ Box corners
		public static float KochanekBartelsLerp (float x0, float y0, float x1, float y1, float x2, float y2, float x3, float y3, float tension, float bias, float continuity, float x)
		{
			// d_i = (1-t) * (1+b) * (1+c) / 2 * (p_i-p_i-1) + (1-t) * (1-b) * (1-c) / 2 * (p_i+1-p_i)
			// d_i+1 = (1-t) * (1+b) * (1-c) / 2 * (p_i+1-p_i) + (1-t) * (1-b) * (1+c) / 2 * (p_i+2-p_i+1)

			float d1 = (1 - tension) * (1 + bias) * (1 + continuity) / 2 * (y1 - y0) + (1 - tension) * (1 - bias) * (1 - continuity) / 2 * (y2 + y1);
			float d2 = (1 - tension) * (1 + bias) * (1 - continuity) / 2 * (y2 - y1) + (1 - tension) * (1 - bias) * (1 + continuity) / 2 * (y3 + y2);
		
			return HermiteLerp (x0, y0, y1, y2, d1, d2, x);
		}

		public static float KochanekBartelsLerp (Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float tension, float bias, float continuity, float x)
		{
			return KochanekBartelsLerp (p0.x, p0.y, p1.x, p1.y, p2.x, p2.y, p3.x, p3.y, tension, bias, continuity, x);
		}
	}
}
