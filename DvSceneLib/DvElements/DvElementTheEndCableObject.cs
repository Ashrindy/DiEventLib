using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("The End Cable Object", "Adds the Supreme/The End cable")]
public class DvElementTheEndCableObject : DvNodeElement
{
    public uint Flags = 0;
    public uint Field_04 = 0;
    public float[] CurveData = new float[1024];

    public DvElementTheEndCableObject() : base(DvElementID.TheEndCableObject) { }
    public DvElementTheEndCableObject(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Flags = reader.Read<uint>();
        Field_04 = reader.Read<uint>();
        CurveData = reader.ReadArray<float>(1024);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        writer.Write(Flags);
        writer.Write(Field_04);
        writer.WriteArray(CurveData);
    }
}
