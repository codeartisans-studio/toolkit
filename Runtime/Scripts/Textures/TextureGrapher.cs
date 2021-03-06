﻿using System;
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

namespace Toolkit
{
    public class TextureGrapher
    {
        public Texture2D texture { get; }

        public LineType lineType = LineType.Solid;
        public int dotInterval = 5;
        public int dashInterval = 5;
        public bool alphaBlend = true;

        private Rect graphRect;
        private Color color;

        private Color[] pixels;
        private Color[] buffer;
        private int pixelWidth;
        private int pixelHeight;
        private float scaleX;
        private float scaleY;
        private float srcAlpha;
        private float dstAlpha;
        private int step;
        private bool dirty;

        public TextureGrapher(int pixelWidth, int pixelHeight, Rect graphRect)
        {
            this.pixelWidth = pixelWidth;
            this.pixelHeight = pixelHeight;
            this.GraphRect = graphRect;

            texture = new Texture2D(pixelWidth, pixelHeight, TextureFormat.RGBA32, false);
            pixels = new Color[pixelWidth * pixelHeight];
        }

        public TextureGrapher(int pixelWidth, int pixelHeight) : this(pixelWidth, pixelHeight, new Rect(0, 0, pixelWidth, pixelHeight))
        {

        }

        public TextureGrapher(int pixelWidth, int pixelHeight, float graphWidth, float graphHeight) : this(pixelWidth, pixelHeight, new Rect(0, 0, graphWidth, graphHeight))
        {

        }

        public Rect GraphRect
        {
            get
            {
                return graphRect;
            }
            set
            {
                graphRect = value;

                scaleX = pixelWidth / graphRect.width;
                scaleY = pixelHeight / graphRect.height;
            }
        }

        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;

                srcAlpha = color.a;
                dstAlpha = 1 - srcAlpha;
            }
        }

        private bool CheckPixel()
        {
            if (lineType == LineType.Solid)
            {
                return true;
            }

            if (lineType == LineType.Dotted)
            {
                return (step++ % dotInterval) == 0;
            }

            if (lineType == LineType.Dashed)
            {
                return (step++ / dashInterval % 2) == 0;
            }

            return true;
        }

        public void Destroy()
        {
            Object.DestroyImmediate(texture);
        }

        public void Clear()
        {
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = Color;
            }

            dirty = true;
        }

        public void Save()
        {
            if (buffer != null)
            {
                pixels.CopyTo(buffer, 0);
            }
            else
            {
                buffer = pixels.Clone() as Color[];
            }
        }

        public void Restore()
        {
            if (buffer != null)
            {
                buffer.CopyTo(pixels, 0);

                dirty = true;
            }
        }

        public void Apply()
        {
            if (dirty)
            {
                texture.SetPixels(pixels);
                texture.Apply(false);

                dirty = false;
            }
        }

        // pixel tp graph methods
        public float Pixel2GraphX(int pixelX)
        {
            return GraphRect.xMin + pixelX / scaleX;
        }

        public float Pixel2GraphY(int pixelY)
        {
            return GraphRect.yMin + pixelY / scaleY;
        }

        public float Pixel2GraphWidth(int pixelWidth)
        {
            return pixelWidth / scaleX;
        }

        public float Pixel2GraphHeight(int pixelHeight)
        {
            return pixelHeight / scaleY;
        }

        // graph to pixel methods
        public int Graph2PixelX(float graphX)
        {
            return Mathf.RoundToInt((graphX - GraphRect.xMin) * scaleX);
        }

        public int Graph2PixelY(float graphY)
        {
            return Mathf.RoundToInt((graphY - GraphRect.yMin) * scaleY);
        }

        public int Graph2PixelWidth(float graphWidth)
        {
            return Mathf.RoundToInt(graphWidth * scaleX);
        }

        public int Graph2PixelHeight(float graphHeight)
        {
            return Mathf.RoundToInt(graphHeight * scaleY);
        }

        // pixel methods
        public Color AlphaBlend(Color dst)
        {
            return new Color(
                Color.r * srcAlpha + dst.r * dstAlpha,
                Color.g * srcAlpha + dst.g * dstAlpha,
                Color.b * srcAlpha + dst.b * dstAlpha,
                Color.a * srcAlpha + dst.a * dstAlpha
            );
        }

        public Color GetPixel(int pixelX, int pixelY)
        {
            return pixels[pixelY * texture.width + pixelX];
        }

        public void SetPixel(int pixelX, int pixelY)
        {
            if (pixelX >= 0 && pixelX < texture.width && pixelY >= 0 && pixelY < texture.height)
            {
                int i = pixelY * texture.width + pixelX;

                pixels[i] = alphaBlend ? AlphaBlend(pixels[i]) : Color;

                dirty = true;
            }
        }

        public void HorizontalSegment(int pixelY, int pixelX0, int pixelX1)
        {
            step = 0;

            if (pixelX1 < pixelX0)
            {
                int swap = pixelX1;
                pixelX1 = pixelX0;
                pixelX0 = swap;
            }

            for (int x = pixelX0; x < pixelX1; x++)
            {
                if (CheckPixel())
                {
                    SetPixel(x, pixelY);
                }
            }

            dirty = true;
        }

        public void VerticalSegment(int pixelX, int pixelY0, int pixelY1)
        {
            step = 0;

            if (pixelY1 < pixelY0)
            {
                int swap = pixelY1;
                pixelY1 = pixelY0;
                pixelY0 = swap;
            }

            for (int y = pixelY0; y < pixelY1; y++)
            {
                if (CheckPixel())
                {
                    SetPixel(pixelX, y);
                }
            }

            dirty = true;
        }

        public void Ellipse(int pixelCenterX, int pixelCenterY, int pixelRadiusX, int pixelRadiusY)
        {
            step = 0;

            for (float i = 0; i < Math.PI * 2; i += 0.1f)
            {
                int x = Mathf.RoundToInt(pixelCenterX + Mathf.Cos(i) * pixelRadiusX);
                int y = Mathf.RoundToInt(pixelCenterY + Mathf.Sin(i) * pixelRadiusY);

                if (CheckPixel())
                {
                    SetPixel(x, y);
                }
            }

            dirty = true;
        }

        // graph methods
        public void DrawHorizontalSegment(float graphY, float graphX0, float graphX1)
        {
            int x0 = Graph2PixelX(graphX0);
            int x1 = Graph2PixelX(graphX1);
            int y = Graph2PixelY(graphY);

            HorizontalSegment(y, x0, x1);

            dirty = true;
        }

        public void DrawVerticalSegment(float graphX, float graphY0, float graphY1)
        {
            int y0 = Graph2PixelY(graphY0);
            int y1 = Graph2PixelY(graphY1);
            int x = Graph2PixelX(graphX);

            VerticalSegment(x, y0, y1);

            dirty = true;
        }

        public void DrawHorizontalLine(float graphY)
        {
            DrawHorizontalSegment(graphY, GraphRect.xMin, GraphRect.xMax);

            dirty = true;
        }

        public void DrawVerticalLine(float graphX)
        {
            DrawVerticalSegment(graphX, GraphRect.yMin, GraphRect.yMax);

            dirty = true;
        }

        public void DrawGrid(float graphStepX, float graphStepY)
        {
            if (graphStepX < Pixel2GraphX(2))
                graphStepX = Pixel2GraphX(2);
            if (graphStepY < Pixel2GraphY(2))
                graphStepY = Pixel2GraphY(2);

            for (float x = (int)(GraphRect.xMin / graphStepX) * graphStepX; x < GraphRect.xMax; x += graphStepX)
            {
                DrawVerticalLine(x);
            }

            for (float y = (int)(GraphRect.yMin / graphStepY) * graphStepY; y < GraphRect.yMax; y += graphStepY)
            {
                DrawHorizontalLine(y);
            }

            dirty = true;
        }

        public void DrawCross(float graphX, float graphY, int pixelRadiusX = 1, int pixelRadiusY = 1)
        {
            int centerX = Graph2PixelX(graphX);
            int centerY = Graph2PixelY(graphY);

            HorizontalSegment(centerY, centerX - pixelRadiusX, centerX + pixelRadiusX);
            VerticalSegment(centerX, centerY - pixelRadiusX, centerY + pixelRadiusY);

            dirty = true;
        }

        public void DrawRect(float graphX, float graphY, float graphWidth, float graphHeight)
        {
            int x0 = Graph2PixelX(graphX);
            int y0 = Graph2PixelY(graphY);
            int x1 = Graph2PixelX(graphX + graphWidth);
            int y1 = Graph2PixelY(graphY + graphHeight);

            HorizontalSegment(y0, x0, x1);
            HorizontalSegment(y1, x0, x1);
            VerticalSegment(x0, y0, y1);
            VerticalSegment(x1, y0, y1);

            dirty = true;
        }

        public void FillRect(float graphX, float graphY, float graphWidth, float graphHeight)
        {
            int x0 = Graph2PixelX(graphX);
            int y0 = Graph2PixelY(graphY);
            int x1 = Graph2PixelX(graphX + graphWidth);
            int y1 = Graph2PixelY(graphY + graphHeight);

            if (y1 < y0)
            {
                int swap = y1;
                y1 = y0;
                y0 = swap;
            }

            for (int y = y0; y < y1; y++)
            {
                HorizontalSegment(y, x0, x1);
            }

            dirty = true;
        }

        public void DrawCircle(float graphCenterX, float graphCenterY, int pixelRadius = 1)
        {
            int centerX = Graph2PixelX(graphCenterX);
            int centerY = Graph2PixelY(graphCenterY);

            Ellipse(centerX, centerY, pixelRadius, pixelRadius);

            dirty = true;
        }

        public void DrawEllipse(float graphCenterX, float graphCenterY, int graphRadiusX, int graphRadiusY)
        {
            int centerX = Graph2PixelX(graphCenterX);
            int centerY = Graph2PixelY(graphCenterY);
            int radiusX = Graph2PixelWidth(graphRadiusX);
            int radiusY = Graph2PixelWidth(graphRadiusY);

            Ellipse(centerX, centerY, radiusX, radiusY);

            dirty = true;
        }

        public void DrawFunction(Func<float, float> function, float graphX0, float graphX1)
        {
            step = 0;

            int x0 = Graph2PixelX(graphX0);
            int x1 = Graph2PixelX(graphX1);

            for (int x = x0; x < x1; x++)
            {
                if (CheckPixel())
                {
                    SetPixel(x, Graph2PixelY(function(Pixel2GraphX(x))));
                }
            }

            dirty = true;
        }

        public void DrawFunction(Func<float, float> function)
        {
            DrawFunction(function, GraphRect.xMin, GraphRect.xMax);

            dirty = true;
        }
    }
}
