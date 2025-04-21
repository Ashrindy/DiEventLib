using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementBloom : DvNodeObject
{
    public uint Field_00 { get; set; } = 0;
    public float Field_04 { get; set; } = 0;
    public float Field_08 { get; set; } = 0;
    public uint Field_10 { get; set; } = 0;
    public float Field_14 { get; set; } = 0;
    public float[] Field_18 { get; set; }
    public float[] CurveData { get; set; }
    public DvElementBloom() 
    {
        Field_18 = new float[6];
        for(int i = 0; i < 6; i++)
        {
            Field_18[i] = 0;
        }
        CurveData = new float[32];
        for(int i = 0; i < 32; i++) 
        {
            CurveData[i] = 1;
        }
    }
    public DvElementBloom(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        Field_04 = reader.Read<float>();
        Field_08 = reader.Read<float>();
        Field_10 = reader.Read<uint>();
        Field_14 = reader.Read<float>();
        Field_18 = reader.ReadArray<float>(6);
        CurveData = reader.ReadArray<float>(32);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.Write(Field_04);
        writer.Write(Field_08);
        writer.Write(Field_10);
        writer.Write(Field_14);
        writer.WriteArray(Field_18);
        writer.WriteArray(CurveData);
    }
}
