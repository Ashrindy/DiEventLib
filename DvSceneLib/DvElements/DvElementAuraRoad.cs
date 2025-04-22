using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Aura Road", "Wyvern's aura road")]
public class DvElementAuraRoad : DvNodeElement
{
    public uint Field_00 = 0;
    public float[] CurveData0 = new float[32];
    public float[] CurveData1 = new float[32];

    public DvElementAuraRoad() : base(DvElementID.AuraRoad) { }
    public DvElementAuraRoad(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        CurveData0 = reader.ReadArray<float>(32);
        CurveData1 = reader.ReadArray<float>(32);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.WriteArray(CurveData0);
        writer.WriteArray(CurveData1);
    }
}