using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementShadowMapParam : DvNodeObject
{
    public uint[] Field_00 { get; set; }
    public float[] Field_04 { get; set; }
    public uint Field_05 { get; set; }
    public float[] Data { get; set; }
    public uint[] Field_08 { get; set; }
    public uint ShadowMapRes1 { get; set; }
    public uint ShadowMapRes2 { get; set; }
    public float[] Field_10 { get; set; }
    public DvElementShadowMapParam() { }
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
