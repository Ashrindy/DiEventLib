using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementShadowMapParam : DvNodeObject
{
    public uint[] Field_00 { get; set; }
    public float[] Field_04 { get; set; }
    public uint Field_05 { get; set; } = 0;
    public float[] Data { get; set; }
    public uint[] Field_08 { get; set; }
    public uint ShadowMapRes1 { get; set; } = 0;
    public uint ShadowMapRes2 { get; set; } = 0;
    public float[] Field_10 { get; set; }
    public DvElementShadowMapParam() 
    {
        Field_00 = new uint[5];
        for (int i = 0; i < 5; i++)
        {
            Field_00[i] = 0;
        }
        Field_04 = new float[9];
        for (int i = 0; i < 9; i++)
        {
            Field_04[i] = 0;
        }
        Data = new float[8];
        for (int i = 0; i < 8; i++)
        {
            Data[i] = 0;
        }
        Field_08 = new uint[3];
        for (int i = 0; i < 3; i++)
        {
            Field_08[i] = 0;
        }
        Field_10 = new float[5];
        for (int i = 0; i < 5; i++)
        {
            Field_10[i] = 0;
        }
    }
    public DvElementShadowMapParam(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.ReadArray<uint>(5);
        Field_04 = reader.ReadArray<float>(9);
        Field_05 = reader.Read<uint>();
        Data = reader.ReadArray<float>(8);
        Field_08 = reader.ReadArray<uint>(3);
        ShadowMapRes1 = reader.Read<uint>();
        ShadowMapRes2 = reader.Read<uint>();
        Field_10 = reader.ReadArray<float>(5);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.WriteArray(Field_00);
        writer.WriteArray(Field_04);
        writer.Write(Field_05);
        writer.WriteArray(Data);
        writer.WriteArray(Field_08);
        writer.Write(ShadowMapRes1);
        writer.Write(ShadowMapRes2);
        writer.WriteArray(Field_10);
    }
}
