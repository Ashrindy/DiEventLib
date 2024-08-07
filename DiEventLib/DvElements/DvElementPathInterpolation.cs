using Amicitia.IO.Binary;
using DiEventLib.Misc;

namespace DiEventLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Path Interpolation", "Interpolates between path adjustments")]
public class DvElementPathInterpolation : DvNodeElement
{
    public byte[] Data;

    public DvElementPathInterpolation() : base(DvElementID.PathInterpolation)
    {
        Data = new byte[592];
        for (int i = 0; i < 592; i++)
        {
            Data[i] = 0;
        }
    }
    public DvElementPathInterpolation(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Data = reader.ReadArray<byte>(592);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.WriteArray(Data);
    }
}
