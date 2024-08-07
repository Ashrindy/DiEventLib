using Amicitia.IO.Binary;
using DiEventLib.Misc;

namespace DiEventLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Time", "Changes the time of day/night")]
public class DvElementTime : DvNodeElement
{
    public uint Field_00 = 0;
    public uint Flags = 0;
    public uint Field_08 = 0;
    public uint Field_0c = 0;
    public uint Field_10 = 0;
    public uint Field_14 = 0;
    public uint Field_18 = 0;
    public uint Field_1c = 0;
    public uint Field_20 = 0;
    public float[] CurveData;

    public DvElementTime() : base(DvElementID.Time)
    {
        CurveData = new float[32];
        for (int i = 0; i < 32; i++)
        {
            CurveData[i] = 1;
        }
    }
    public DvElementTime(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        Flags = reader.Read<uint>();
        Field_08 = reader.Read<uint>();
        Field_0c = reader.Read<uint>();
        Field_10 = reader.Read<uint>();
        Field_14 = reader.Read<uint>();
        Field_18 = reader.Read<uint>();
        Field_1c = reader.Read<uint>();
        Field_20 = reader.Read<uint>();
        CurveData = reader.ReadArray<float>(32);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.Write(Flags);
        writer.Write(Field_08);
        writer.Write(Field_0c);
        writer.Write(Field_10);
        writer.Write(Field_14);
        writer.Write(Field_18);
        writer.Write(Field_1c);
        writer.Write(Field_20);
        writer.WriteArray(CurveData);
    }
}
