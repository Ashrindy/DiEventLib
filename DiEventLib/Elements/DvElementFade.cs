using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Elements
{
    public struct RGBA32
    {
        public uint A;
        public uint B;
        public uint G;
        public uint R;
    }

    public struct fade
    {
        public RGBA32 color;
        public float[] curveData;
    }
}
