using Amicitia.IO.Binary;
using DiEventLib.Misc;

namespace DiEventLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Point Light", "Adds a point light for lighting up the cutscene")]
public class DvElementVariablePointLight : DvNodeElement
{
    public float[] Unk1;
    public int[] Unk2;
    public float[] Unk3;
    public int Unk4 = 0;
    public int[] Unk5;
    public float[] CurveData;
    public DvElementVariablePointLight() : base(DvElementID.VariablePointLight) { }
    public DvElementVariablePointLight(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Unk1 = reader.ReadArray<float>(7);
        Unk2 = reader.ReadArray<int>(6);
        Unk3 = reader.ReadArray<float>(8);
        Unk4 = reader.Read<int>();
        Unk5 = reader.ReadArray<int>(10);
        CurveData = reader.ReadArray<float>(128);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.WriteArray(Unk1);
        writer.WriteArray(Unk2);
        writer.WriteArray(Unk3);
        writer.Write(Unk4);
        writer.WriteArray(Unk5);
        writer.WriteArray(CurveData);
    }
}
