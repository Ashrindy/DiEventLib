using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public struct gameCam
    {
        public float[] field_4c;
    }

    public class DvElementGameCamera : DvNodeObject
    {
        public gameCam gameCam;

        public DvElementGameCamera(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            gameCam.field_4c = new float[26];
            for (int i = 0; i < 26; i++)
            {
                gameCam.field_4c[i] = reader.ReadSingle();
            }
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            elementProperties prop = ((DvElement)Node.info).elementInfo;
            gameCam elementGameCam = ((DvElementGameCamera)prop.info).gameCam;
            foreach (var i in elementGameCam.field_4c)
            {
                Writer.Write(i);
            }
        }
    }
}
