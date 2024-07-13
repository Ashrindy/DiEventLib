using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementColorContrast : DvNodeElement
{
    public uint Field_00 { get; set; } = 0;
    public float Field_04 { get; set; } = 0;
    public float Field_08 { get; set; } = 0;
    public float Field_0c { get; set; } = 0;
    public float Field_1c { get; set; } = 0;
    public uint Field_01 { get; set; } = 0;
    public float Field_2c { get; set; } = 0;
    public uint Field_02 { get; set; } = 0;
    public float[] CurveData { get; set; }
    public DvElementColorContrast() 
    {
        CurveData = new float[32];
        for(int i = 0; i < 32; i++)
        {
            CurveData[i] = 1;
        }
    }
    public DvElementColorContrast(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        Field_04 = reader.Read<float>();
        Field_08 = reader.Read<float>();
        Field_0c = reader.Read<float>();
        Field_1c = reader.Read<float>();
        Field_01 = reader.Read<uint>();
        Field_2c = reader.Read<float>();
        Field_02 = reader.Read<uint>();
        CurveData = reader.ReadArray<float>(32);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.Write(Field_04);
        writer.Write(Field_08);
        writer.Write(Field_0c);
        writer.Write(Field_1c);
        writer.Write(Field_01);
        writer.Write(Field_2c);
        writer.Write(Field_02);
        writer.WriteArray(CurveData);
    }
}
