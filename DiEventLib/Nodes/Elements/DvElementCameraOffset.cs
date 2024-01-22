﻿using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public struct cameraOffset
    {
        public float[] data;
        public float[] animData;
    }

    public class DvElementCameraOffset : DvNodeObject
    {
        public cameraOffset cameraOffset;

        public DvElementCameraOffset(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            cameraOffset.data = new float[12];
            for (int i = 0; i < 12; i++)
            {
                cameraOffset.data[i] = reader.ReadSingle();
            }

            cameraOffset.animData = new float[256];
            for (int i = 0; i < 256; i++)
            {
                cameraOffset.animData[i] = reader.ReadSingle();
            }
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            Helper.WriteMatrix(Writer, ((DvPath)Node.info).rootPath.matrix);
            Writer.Write(((DvPath)Node.info).rootPath.flag);

            foreach (var i in ((DvPath)Node.info).rootPath.padding)
            {
                Writer.Write(i);
            }
        }
    }
}
