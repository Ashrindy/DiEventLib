using Amicitia.IO.Binary;
using System.Numerics;
using System.Text;

namespace DiEventLib;

public class DvElementTime : DvNodeObject
{
    public uint Field_00 { get; set; }
    public uint Flags { get; set; }
    public uint Field_08 { get; set; }
    public uint Field_0c { get; set; }
    public uint Field_10 { get; set; }
    public uint Field_14 { get; set; }
    public uint Field_18 { get; set; }
    public uint Field_1c { get; set; }
    public uint Field_20 { get; set; }
    public float[] CurveData { get; set; }

    public DvElementTime() { }
    public DvElementTime(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
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

    public override void Write(BinaryObjectWriter writer)
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
