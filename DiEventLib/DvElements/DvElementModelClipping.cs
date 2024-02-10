using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementModelClipping : DvNodeObject
{
    public byte[] Data { get; set; }
    public DvElementModelClipping() { }
    public DvElementModelClipping(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Data = reader.ReadArray<byte>(20);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.WriteArray(Data);
    }
}
