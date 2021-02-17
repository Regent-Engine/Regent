using System;
using System.Collections.Generic;
using System.Text;

namespace Redux.Media
{
    public sealed class Color
    {
        public Color(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public byte R
        {
            get;
            set;
        }

        public byte G
        {
            get;
            set;
        }

        public byte B
        {
            get;
            set;
        }
        public byte A
        {
            get;
            set;
        }
    }
}
