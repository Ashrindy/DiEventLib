using Amicitia.IO.Binary;
using DvSceneLib.Misc;
using System;
using System.Numerics;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Density Sector Point", "")]
public class DvElementDensitySectorPoint : DvNodeElement
{
    public uint Field_00 = 0;
    public Vector3 Unk0 = new();
    public float[] Unk1 = new float[4];

    public DvElementDensitySectorPoint() : base(DvElementID.DensitySectorPoint) { }
    public DvElementDensitySectorPoint(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        Unk0 = reader.Read<Vector3>();
        Unk1 = reader.ReadArray<float>(4);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.Write(Unk0);
        writer.WriteArray(Unk1);
    }
}
