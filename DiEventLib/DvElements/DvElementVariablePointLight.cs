using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvElementVariablePointLight : DvNodeObject
{
    public float[] Unk1 { get; set; }
    public int[] Unk2 { get; set; }
    public float[] Unk3 { get; set; }
    public int Unk4 { get; set; }
    public int[] Unk5 { get; set; }
    public float[] Data { get; set; }
    public DvElementVariablePointLight() { }
    public DvElementVariablePointLight(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Unk1 = reader.ReadArray<float>(7);
        Unk2 = reader.ReadArray<int>(6);
        Unk3 = reader.ReadArray<float>(8);
        Unk4 = reader.Read<int>();
        Unk5 = reader.ReadArray<int>(10);
        Data = reader.ReadArray<float>(128);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.WriteArray(Unk1);
        writer.WriteArray(Unk2);
        writer.WriteArray(Unk3);
        writer.Write(Unk4);
        writer.WriteArray(Unk5);
        writer.WriteArray(Data);
    }
}
