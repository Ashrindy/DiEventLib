using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("CyberSpace Noise", "A glitchy cyberspace type overlay")]
public class DvElementCyberSpaceNoise : DvNodeElement
{
    public uint Flags = 0;
    public float[] CurveData = new float[32];

    public DvElementCyberSpaceNoise() : base(DvElementID.CyberSpaceNoise) { }
    public DvElementCyberSpaceNoise(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Flags = reader.Read<uint>();
        CurveData = reader.ReadArray<float>(32);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        writer.Write(Flags);
        writer.WriteArray(CurveData);
    }
}
