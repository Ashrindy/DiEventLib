using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementFade : DvNodeObject
{
    public RGBA32 Color { get; set; }
    public float[] CurveData { get; set; }

    public DvElementFade() 
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
    public override void Read(BinaryObjectReader reader)
    {
        Color = reader.Read<RGBA32>();
        CurveData = reader.ReadArray<float>(32);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Color);
        writer.WriteArray(CurveData);
    }
}
