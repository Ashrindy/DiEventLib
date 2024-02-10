using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementCyberSpaceNoise : DvNodeObject
{
    public uint Flags {  get; set; }
    public float[] CurveData {  get; set; }
    public DvElementCyberSpaceNoise() { }
    public DvElementCyberSpaceNoise(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Flags = reader.Read<uint>();
        CurveData = reader.ReadArray<float>(32);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Flags);
        writer.WriteArray(CurveData);
    }
}
