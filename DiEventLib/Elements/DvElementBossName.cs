using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Elements
{
    public enum bossID
    {
        giganto = 0,
        wyvern,
        knight,
        supreme,
        theEnd,
        supremeTheEnd
    }

    public struct bossName
    {
        public uint field_00;
        public bossID bossID;
    }
}
