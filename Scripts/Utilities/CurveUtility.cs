using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit.Utilities
{
	[Serializable]
	public struct CurvePoint
	{
		public int x;
		public int y;
		public float inTangent;
		public float outTangent;

		public CurvePoint (int x, int y, float inTangent = 0, float outTangent = 0)
		{
			this.x = x;
			this.y = y;
			this.inTangent = inTangent;
			this.outTangent = outTangent;
		}
	}

	public static class CurveUtility
	{
		// Cubic Bézier curves
		public static float BezierLerp (float p0, float p1, float p2, float p3, float t)
		{
			// B(t) = (1-t)^3p_0 + 3t(1-t)^2p_1 + 3t^2(1-t)p_2 + t^3p_3
			// => -p_0t^3 + 3p_1t^3 - 3p_2t^3 + p_3t^3 + 3p_0t^2 - 6p_1t^2 + 3p_2t^2 - 3p_0t + 3p_1t + p_0
			// => (-p_0 + 3p_1 - 3p_2 + p_3)t^3 + (3p_0 - 6p_1 + 3p_2)t^2 + (-3p_0 + 3p_1)t + p_0

			float t2 = t * t;
			float t3 = t * t2;

			float cx = 3.0f * (p1 - p0);
			float bx = 3.0f * (p2 - p1) - cx;
			float ax = p3 - p0 - cx - bx;

			return (ax * t3) + (bx * t2) + (cx * t) + p0;
		}

		public static Vector2 BezierLerp (Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t)
		{
			return new Vector2 (BezierLerp (p0.x, p1.x, p2.x, p3.x, t), BezierLerp (p0.y, p1.y, p2.y, p3.y, t));
		}

		// Cubic Hermite spline (Unit interval (0, 1))
		public static float HermiteLerp (float p0, float p1, float m0, float m1, float t)
		{
			// h_{00}(t) = 2t^3 - 3t^2 + 1
			// h_{10}(t) = t^3 - 2t^2 + t
			// h_{01}(t) = -2t^3 + 3t^2
			// h_{11}(t) = t^3 - t^2

			// p(t) = h_{00}(t)p_0 + h_{10}(t)m_0 + h_{01}(t)p_1 + h_{11}(t)m_1

			float t2 = t * t;
			float t3 = t * t2;

			return (2 * t3 - 3 * t2 + 1) * p0 + (t3 - 2 * t2 + t) * m0 + (-2 * t3 + 3 * t2) * p1 + (t3 - t2) * m1;
		}

		// Cubic Hermite spline (Interpolation on an arbitrary interval)
		public static float HermiteLerp (float x0, float y0, float x1, float y1, float outTangent, float inTangent, float x)
		{
			// t = (x - x_k) / (x_{k+1} - x_k)
			// p(x) = h_{00}(t)p_k + h_{10}(t)(x_{k+1} - x_k)m_k + h_{01}(t)p_{k+1} + h_{11}(t)(x_{k+1} - x_k)m_{k+1}

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

		public static float HermiteLerp (CurvePoint p0, CurvePoint p1, float x)
		{
			return HermiteLerp (p0.x, p0.y, p1.x, p1.y, p0.outTangent, p1.inTangent, x);
		}

		public static float HermiteLerp (CurvePoint[] points, float x)
		{
			float y	= 0;

			for (int i = 0; i < points.Length - 1; i++) {
				CurvePoint p0 = points [i];
				CurvePoint p1 = points [i + 1];
				
				if (p0.x <= x && x < p1.x) {
					y = HermiteLerp (p0, p1, x);
				}
			}

			return y;
		}

		// Catmull–Rom spline
		public static float CatmullRomLerp (float p0, float p1, float p2, float p3, float t)
		{
			// m_k = (p_{k+1} - p_{k-1}) / 2

			float m1 = (p2 - p0) / 2;
			float m2 = (p3 - p1) / 2;

			return HermiteLerp (p1, p2, m1, m2, t);
		}

		public static float CatmullRomLerp (float x0, float y0, float x1, float y1, float x2, float y2, float x3, float y3, float x)
		{
			// m_k = (p_{k+1} - p_{k-1}) / (t_{k+1} - t_{k-1})

			float m1 = (y2 - y0) / (x2 - x0);
			float m2 = (y3 - y1) / (x3 - x1);

			return HermiteLerp (x1, y2, y1, y2, m1, m2, x);
		}

		public static float CatmullRomLerp (Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float x)
		{
			return CatmullRomLerp (p0.x, p0.y, p1.x, p1.y, p2.x, p2.y, p3.x, p3.y, x);
		}

		public static float CatmullRomLerp (Vector2[] points, float x)
		{
			float y	= 0;

			for (int i = 1; i < points.Length - 2; i++) {
				Vector2 p0 = points [i - 1];
				Vector2 p1 = points [i];
				Vector2 p2 = points [i + 1];
				Vector2 p3 = points [i + 2];

				if (p1.x <= x && x < p2.x) {
					y = CatmullRomLerp (p0, p1, p2, p3, x);
				}
			}

			return y;
		}

		// Cardinal spline
		public static float CardinalLerp (float p0, float p1, float p2, float p3, float tension, float t)
		{
			// m_k = (1-c) * (p_{k+1} - p_{k-1}) / 2

			float m1 = (1 - tension) * (p2 - p0) / 2;
			float m2 = (1 - tension) * (p3 - p1) / 2;

			return HermiteLerp (p1, p2, m1, m2, t);
		}

		public static float CardinalLerp (float x0, float y0, float x1, float y1, float x2, float y2, float x3, float y3, float tension, float x)
		{
			// m_k = (1-c) * (p_{k+1} - p_{k-1}) / (t_{k+1} - t_{k-1})

			float m1 = (1 - tension) * (y2 - y0) / (x2 - x0);
			float m2 = (1 - tension) * (y3 - y1) / (x3 - x1);

			return HermiteLerp (x1, y2, y1, y2, m1, m2, x);
		}

		public static float CardinalLerp (Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float tension, float x)
		{
			return CardinalLerp (p0.x, p0.y, p1.x, p1.y, p2.x, p2.y, p3.x, p3.y, tension, x);
		}

		public static float CardinalLerp (Vector2[] points, float tension, float x)
		{
			float y	= 0;

			for (int i = 1; i < points.Length - 2; i++) {
				Vector2 p0 = points [i - 1];
				Vector2 p1 = points [i];
				Vector2 p2 = points [i + 1];
				Vector2 p3 = points [i + 2];

				if (p1.x <= x && x < p2.x) {
					y = CardinalLerp (p0, p1, p2, p3, tension, x);
				}
			}

			return y;
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
			// d_i = (1-t)(1+b)(1+c) / 2 * (p_i - p_{i-1}) + (1-t)(1-b)(1-c) / 2 * (p_{i+1} - p_i)
			// d_{i+1} = (1-t)(1+b)(1-c) / 2 * (p_{i+1} - p_i) + (1-t)(1-b)(1+c) / 2 * (p_{i+2} - p_{i+1})

			float d1 = (1 - tension) * (1 + bias) * (1 + continuity) / 2 * (y1 - y0) + (1 - tension) * (1 - bias) * (1 - continuity) / 2 * (y2 + y1);
			float d2 = (1 - tension) * (1 + bias) * (1 - continuity) / 2 * (y2 - y1) + (1 - tension) * (1 - bias) * (1 + continuity) / 2 * (y3 + y2);
		
			return HermiteLerp (x0, y0, y1, y2, d1, d2, x);
		}

		public static float KochanekBartelsLerp (Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float tension, float bias, float continuity, float x)
		{
			return KochanekBartelsLerp (p0.x, p0.y, p1.x, p1.y, p2.x, p2.y, p3.x, p3.y, tension, bias, continuity, x);
		}

		public static float KochanekBartelsLerp (Vector2[] points, float tension, float bias, float continuity, float x)
		{
			float y	= 0;

			for (int i = 1; i < points.Length - 2; i++) {
				Vector2 p0 = points [i - 1];
				Vector2 p1 = points [i];
				Vector2 p2 = points [i + 1];
				Vector2 p3 = points [i + 2];

				if (p1.x <= x && x < p2.x) {
					y = KochanekBartelsLerp (p0, p1, p2, p3, tension, bias, continuity, x);
				}
			}

			return y;
		}
	}
}
