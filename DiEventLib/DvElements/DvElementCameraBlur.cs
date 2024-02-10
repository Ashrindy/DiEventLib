using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementCameraBlur : DvNodeObject
{
    public uint Flags { get; set; }
    public uint BlurAmount { get; set; }
    public float[] CurveData { get; set; }

    public DvElementCameraBlur() { }
    public DvElementCameraBlur(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Flags = reader.Read<uint>();
        BlurAmount = reader.Read<uint>();
        CurveData = reader.ReadArray<float>(34);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Flags);
        writer.Write(BlurAmount);
        writer.WriteArray(CurveData);
    }
}
