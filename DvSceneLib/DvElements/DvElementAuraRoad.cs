using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Aura Road", "Wyvern's aura road")]
public class DvElementAuraRoad : DvNodeElement
{
    public uint Field_00 = 0;
    public float[] AnimData;

    public DvElementAuraRoad() : base(DvElementID.AuraRoad)
    { 
        AnimData = new float[64];
        for(int i = 0; i < 64; i++)
        {
            AnimData[i] = 1;
        }
    }
    public DvElementAuraRoad(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        AnimData = reader.ReadArray<float>(64);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.WriteArray(AnimData);
    }
}