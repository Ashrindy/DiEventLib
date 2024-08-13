using Amicitia.IO.Binary;
using DiEventLib.Misc;

namespace DiEventLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Path Interpolation", "Interpolates path movement")]
public class DvElementPathInterpolation : DvNodeElement
{
    public float[] Data;
    public float[] CurveData;

    public DvElementPathInterpolation() : base(DvElementID.PathInterpolation)
    {
        Data = new float[20];
        for (int i = 0; i < 20; i++)
        {
            Data[i] = 0;
        }
        CurveData = new float[128];
        for (int i = 0; i < 128; i++)
        {
            CurveData[i] = 0;
        }
    }
    public DvElementPathInterpolation(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Data = reader.ReadArray<float>(20);
        CurveData = reader.ReadArray<float>(128);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.WriteArray(Data);
        writer.WriteArray(CurveData);
    }
}
