using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public enum LineType
{
	Solid,
	Dotted,
	Dashed
}

namespace Toolkit.Textures
{
	public class TextureGrapher
	{
		public Texture2D texture { get; }

		public Rect graphRect { get; }

		public float scaleX { get; }

		public float scaleY { get; }

		public Color color = Color.white;
		public LineType lineType = LineType.Solid;
		public int dotInterval = 5;
		public int dashInterval = 5;

		private Color[] pixels;
		private Color[] buffer;
		private bool dirty;
		private int step;

		public TextureGrapher (int pixelWidth, int pixelHeight, float xMin, float yMin, float xMax, float yMax)
		{
			texture = new Texture2D (pixelWidth, pixelHeight, TextureFormat.RGBA32, false);
			pixels = new Color[pixelWidth * pixelHeight];

			Rect rect = new Rect ();
			rect.xMin = xMin;
			rect.yMin = yMin;
			rect.xMax = xMax;
			rect.yMax = yMax;
			graphRect = rect;

			scaleX = pixelWidth / graphRect.width;
			scaleY = pixelHeight / graphRect.height;
		}

		public TextureGrapher (int pixelWidth, int pixelHeight) : this (pixelWidth, pixelHeight, 0, 0, pixelWidth, pixelHeight)
		{
			
		}

		private bool CheckPixel ()
		{
			if (lineType == LineType.Solid) {
				return true;
			}

			if (lineType == LineType.Dotted) {
				return (step++ % dotInterval) == 0;
			}

			if (lineType == LineType.Dashed) {
				return (step++ / dashInterval % 2) == 0;
			}

			return true;
		}

		public void Destroy ()
		{
			Object.DestroyImmediate (texture);
		}

		public void Clear ()
		{
			for (int i = 0; i < pixels.Length; i++) {
				pixels [i] = color;
			}

			dirty = true;
		}

		public void Save ()
		{
			if (buffer != null) {
				pixels.CopyTo (buffer, 0);
			} else {
				buffer = pixels.Clone () as Color[];
			}
		}

		public void Restore ()
		{
			if (buffer != null) {
				buffer.CopyTo (pixels, 0);

				dirty = true;
			}
		}

		public void Apply ()
		{
			if (dirty) {
				texture.SetPixels (pixels);
				texture.Apply (false);

				dirty = false;
			}
		}

		// pixel tp graph methods
		public float Pixel2GraphX (int pixelX)
		{
			return pixelX / scaleX;
		}

		public float Pixel2GraphY (int pixelY)
		{
			return pixelY / scaleY;
		}

		public float Pixel2GraphWidth (int pixelWidth)
		{
			return pixelWidth / scaleX;
		}

		public float Pixel2GraphHeight (int pixelHeight)
		{
			return pixelHeight / scaleY;
		}

		// graph to pixel methods
		public int Graph2PixelX (float graphX)
		{
			return Mathf.RoundToInt ((graphX - graphRect.xMin) * scaleX);
		}

		public int Graph2PixelY (float graphY)
		{
			return Mathf.RoundToInt ((graphY - graphRect.yMin) * scaleY);
		}

		public int Graph2PixelWidth (float graphWidth)
		{
			return Mathf.RoundToInt (graphWidth * scaleX);
		}

		public int Graph2PixelHeight (float graphHeight)
		{
			return Mathf.RoundToInt (graphHeight * scaleY);
		}

		// pixel methods
		public Color GetPixel (int pixelX, int pixelY)
		{
			return pixels [pixelY * texture.width + pixelX];
		}

		public void SetPixel (int pixelX, int pixelY)
		{
			if (pixelX >= 0 && pixelX < texture.width && pixelY >= 0 && pixelY < texture.height) {
				pixels [pixelY * texture.width + pixelX] = color;

				dirty = true;
			}
		}

		public void PixelHorizontalSegment (int pixelY, int pixelX0, int pixelX1)
		{
			step = 0;

			for (int x = pixelX0; x <= pixelX1; x++) {
				if (CheckPixel ()) {
					SetPixel (x, pixelY);
				}
			}

			dirty = true;
		}

		public void PixelVerticalSegment (int pixelX, int pixelY0, int pixelY1)
		{
			step = 0;

			for (int y = pixelY0; y <= pixelY1; y++) {
				if (CheckPixel ()) {
					SetPixel (pixelX, y);
				}
			}

			dirty = true;
		}

		public void PixelHorizontalLine (int pixelY)
		{
			PixelHorizontalSegment (pixelY, 0, texture.width);

			dirty = true;
		}

		public void PixelVerticalLine (int pixelX)
		{
			PixelVerticalSegment (pixelX, 0, texture.height);

			dirty = true;
		}

		public void PixelFunction (Func<int, int> function, int pixelX0, int pixelX1)
		{
			step = 0;

			for (int x = pixelX0; x <= pixelX1; x++) {
				if (CheckPixel ()) {
					SetPixel (x, function (x));
				}
			}

			dirty = true;
		}

		public void PixelFunction (Func<int, int> function)
		{
			PixelFunction (function, 0, texture.width);

			dirty = true;
		}

		// graph methods
		public void GraphCross (float graphX, float graphY, int pixelRadiusX = 1, int pixelRadiusY = 1)
		{
			int x = Graph2PixelX (graphX);
			int y = Graph2PixelY (graphY);

			PixelHorizontalSegment (y, x - pixelRadiusX, x + pixelRadiusX);
			PixelVerticalSegment (x, y - pixelRadiusX, y + pixelRadiusY);

			dirty = true;
		}

		public void GraphHorizontalSegment (float graphY, float graphX0, float graphX1)
		{
			int x0 = Graph2PixelX (graphX0);
			int x1 = Graph2PixelX (graphX1);
			int y = Graph2PixelY (graphY);

			PixelHorizontalSegment (y, x0, x1);

			dirty = true;
		}

		public void GraphVerticalSegment (float graphX, float graphY0, float graphY1)
		{
			int y0 = Graph2PixelY (graphY0);
			int y1 = Graph2PixelY (graphY1);
			int x = Graph2PixelX (graphX);

			PixelVerticalSegment (x, y0, y1);

			dirty = true;
		}

		public void GraphHorizontalLine (float graphY)
		{
			int y = Graph2PixelY (graphY);

			PixelHorizontalLine (y);

			dirty = true;
		}

		public void GraphVerticalLine (float graphX)
		{
			int x = Graph2PixelX (graphX);

			PixelVerticalLine (x);

			dirty = true;
		}

		public void GraphGrid (float graphStepX, float graphStepY)
		{
			for (float x = graphRect.xMin + graphStepX; x < graphRect.xMax; x += graphStepX) {
				GraphVerticalLine (x);
			}

			for (float y = graphRect.yMin + graphStepY; y < graphRect.yMax; y += graphStepY) {
				GraphHorizontalLine (y);
			}

			dirty = true;
		}

		public void GraphRect (float graphX, float graphY, float graphWidth, float graphHeight)
		{
			int x0 = Graph2PixelX (graphX);
			int y0 = Graph2PixelY (graphY);
			int x1 = Graph2PixelX (graphX + graphWidth);
			int y1 = Graph2PixelY (graphY + graphHeight);

			for (int x = x0; x <= x1; x++) {
				PixelVerticalSegment (x, y0, y1);
			}

			for (int y = y0; y <= y1; y++) {
				PixelHorizontalSegment (y, x0, x1);
			}

			dirty = true;
		}

		public void GraphFunction (Func<float, float> function, float graphX0, float graphX1)
		{
			step = 0;
			
			int x0 = Graph2PixelX (graphX0);
			int x1 = Graph2PixelX (graphX1);
			
			for (int x = x0; x <= x1; x++) {
				if (CheckPixel ()) {
					SetPixel (x, Graph2PixelY (function (Pixel2GraphX (x))));
				}
			}

			dirty = true;
		}

		public void GraphFunction (Func<float, float> function)
		{
			GraphFunction (function, graphRect.xMin, graphRect.xMax);

			dirty = true;
		}
	}
}
