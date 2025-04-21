using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Fade", "Overlays the screen with a specific color that can be faded in and out")]
public class DvElementFade : DvNodeElement
{
    public RGBA32 Color;
    public float[] CurveData;

    public DvElementFade() : base(DvElementID.Fade)
    {
        Color = new RGBA32
        {
            R = 0,
            G = 0, 
            B = 0, 
            A = 0,
        };
        CurveData = new float[32];
        for(int i = 0; i < 32; i++)
        {
            CurveData[i] = 1;
        }
    }
    public DvElementFade(BinaryObjectReader reader) 
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Color = reader.Read<RGBA32>();
        CurveData = reader.ReadArray<float>(32);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Color);
        writer.WriteArray(CurveData);
    }
}
