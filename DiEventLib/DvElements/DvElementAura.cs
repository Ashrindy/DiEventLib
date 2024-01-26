using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvElementAura : DvNodeObject
{
    public byte[] Data { get; set; }

    public DvElementAura() { }
    public DvElementAura(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Data = reader.ReadArray<byte>(64);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.WriteArray(Data);
    }
}