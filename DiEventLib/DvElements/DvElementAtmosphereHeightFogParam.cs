using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementAtmosphereHeightFogParam : DvNodeElement
{
    public byte[] Data { get; set; }

    public DvElementAtmosphereHeightFogParam() 
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
