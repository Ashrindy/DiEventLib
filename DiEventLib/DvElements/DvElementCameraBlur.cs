using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementCameraBlur : DvNodeObject
{
    public uint Flags { get; set; }
    public uint Field_04 { get; set; }
    public float BlurAmount { get; set; }
    public uint Field_0C { get; set; }
    public float[] CurveData { get; set; }

    public DvElementCameraBlur() { }
    public DvElementCameraBlur(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Flags = reader.Read<uint>();
        Field_04 = reader.Read<uint>();
        BlurAmount = reader.Read<float>();
        Field_0C = reader.Read<uint>();
        CurveData = reader.ReadArray<float>(32);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Flags);
        writer.Write(Field_04);
        writer.Write(BlurAmount);
        writer.Write(Field_0C)
        writer.WriteArray(CurveData);
    }
}
