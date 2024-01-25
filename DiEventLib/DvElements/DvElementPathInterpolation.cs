using Amicitia.IO.Binary;
using System.Numerics;
using System.Text;

namespace DiEventLib;

public class DvElementPathInterpolation : DvNodeObject
{
    public byte[] Data { get; set; }

    public DvElementPathInterpolation() { }
    public DvElementPathInterpolation(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Data = reader.ReadArray<byte>(592);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.WriteArray(Data);
    }
}
