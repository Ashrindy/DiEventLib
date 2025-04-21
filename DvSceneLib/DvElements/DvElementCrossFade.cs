using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementCrossFade : DvNodeObject
{
    public uint Field_00 { get; set; } = 0;
    public uint Field_04 { get; set; } = 0;
    public float[] CurveData { get; set; }
    public DvElementCrossFade() 
    {
        CurveData = new float[32];
        for(int i = 0; i < 32; i++)
        {
            CurveData[i] = 1;
        }
    }
    public DvElementCrossFade(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        Field_04 = reader.Read<uint>();
        CurveData = reader.ReadArray<float>(32);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.Write(Field_04);
        writer.WriteArray(CurveData);
    }
}
