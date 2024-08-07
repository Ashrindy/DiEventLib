using Amicitia.IO.Binary;
using DiEventLib.Misc;

namespace DiEventLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Atmospheric Height Fog", "Modifies the fog")]
public class DvElementAtmosphereHeightFogParam : DvNodeElement
{
    public byte[] Data;

    public DvElementAtmosphereHeightFogParam() : base(DvElementID.AtmosphereHeightFogParam)
    {
        Data = new byte[300];
        for (int i = 0; i < 300; i++)
        {
            Data[i] = 0;
        }
    }
    public DvElementAtmosphereHeightFogParam(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Data = reader.ReadArray<byte>(300);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.WriteArray(Data);
    }
}
