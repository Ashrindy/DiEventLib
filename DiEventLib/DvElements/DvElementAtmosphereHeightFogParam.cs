using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementAtmosphereHeightFogParam : DvNodeObject
{
    public byte[] Data { get; set; }

    public DvElementAtmosphereHeightFogParam() { }
    public DvElementAtmosphereHeightFogParam(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Data = reader.ReadArray<byte>(300);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.WriteArray(Data);
    }
}
