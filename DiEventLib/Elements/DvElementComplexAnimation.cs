using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Elements
{
    public enum animType
    {
        skeletalAnim = 1,
        UVAnim,
        matAnim = 4
    }

    public struct anim
    {
        public animType animType;
        public string filename;
    }

    public struct compAnim
    {
        public uint field_60;
        public byte[] data;
        public uint field_6c;
        public anim[] animations;
        public uint field_03;
    }
}
