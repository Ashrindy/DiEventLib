using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public struct letterBox
    {
        public float[] curveData;
    }

    public class DvElementLetterBox : DvNodeObject
    {
        public letterBox letterBox;

        public DvElementLetterBox(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            letterBox.curveData = new float[32];
            for (int i = 0; i < 32; i++)
            {
                letterBox.curveData[i] = reader.ReadSingle();
            }
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            elementProperties prop = ((DvElement)Node.info).elementInfo;
            letterBox elementLetterBox = ((DvElementLetterBox)prop.info).letterBox;
            foreach (var i in elementLetterBox.curveData)
            {
                Writer.Write(i);
            }
        }
    }
}
