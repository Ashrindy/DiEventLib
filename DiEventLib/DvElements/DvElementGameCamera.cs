﻿using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvElementGameCamera : DvNodeObject
{
    public float[] Field_4c { get; set; }

    public DvElementGameCamera() { }
    public DvElementGameCamera(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Field_4c = reader.ReadArray<float>(26);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.WriteArray(Field_4c);
    }
}
