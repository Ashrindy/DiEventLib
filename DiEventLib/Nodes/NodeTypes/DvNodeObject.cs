using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.NodeTypes
{
    public class DvNodeObject
    {
        public virtual void Read(ExtendedBinaryReader reader)
        {
        }

        public virtual void Write(ExtendedBinaryWriter Writer, node Node)
        {
        }
    }
}
