using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit
{
    [Serializable]
    public struct Coordinate
    {
        public int x;
        public int y;

        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return string.Format("({0}, {1}) ", x, y);
        }
    }
}
