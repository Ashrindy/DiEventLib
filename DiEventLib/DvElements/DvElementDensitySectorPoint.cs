using Amicitia.IO.Binary;
using DiEventLib.Misc;
using System;

namespace DiEventLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Density Sector Point", "")]
public class DvElementDensitySectorPoint : DvNodeElement
{
    public uint Field_00 = 0;
    public float[] Unk01;

    public DvElementDensitySectorPoint() : base(DvElementID.DensitySectorPoint) 
    { 
        Unk01 = new float[7];
        for (int i = 0; i < 7; i++)
        {
            Unk01[i] = 0;
        }
    }
    public DvElementDensitySectorPoint(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        Unk01 = reader.ReadArray<float>(7);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.WriteArray(Unk01);
    }
}
