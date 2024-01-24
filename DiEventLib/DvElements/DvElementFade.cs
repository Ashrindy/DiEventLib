using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvElementFade : DvNodeObject
{
    public RGBA32 Color { get; set; }
    public float[] CurveData { get; set; }

    public DvElementFade() { }
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
