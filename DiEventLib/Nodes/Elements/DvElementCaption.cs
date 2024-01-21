using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public enum languageType
    {
        english = 0,
        french,
        italian,
        german,
        spanish,
        polish,
        portuguese,
        russian,
        japanese,
        chinese,
        chinese_simplified,
        korean
    }

    public struct caption
    {
        public byte[] captionName;
        public languageType languageType;
        public uint padding;
    }
}
