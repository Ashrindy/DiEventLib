using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("CyberSpace Noise", "A glitchy cyberspace type overlay")]
public class DvElementCyberSpaceNoise : DvNodeElement
{
    public uint Flags = 0;
    public float[] CurveData;
    public DvElementCyberSpaceNoise() : base(DvElementID.CyberSpaceNoise)
    { 
        CurveData = new float[32];
        for(int i = 0; i < 32; i++)
        {
            CurveData[i] = 1;
        }
    }
    public DvElementCyberSpaceNoise(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Flags = reader.Read<uint>();
        CurveData = reader.ReadArray<float>(32);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Flags);
        writer.WriteArray(CurveData);
    }
}
