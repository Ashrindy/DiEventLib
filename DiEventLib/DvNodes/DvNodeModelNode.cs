using Amicitia.IO.Binary;
using System.Numerics;
using System.Text;

namespace DiEventLib;

public class DvNodeModelNode : DvNodeObject
{
    public uint Field00 { get; set; }
    public string NodeName { get; set; }

    public DvNodeModelNode() { }
    public DvNodeModelNode(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Field00 = reader.Read<uint>();
        NodeName = reader.ReadString(Encoding.GetEncoding("Shift-JIS"), StringBinaryFormat.FixedLength, 64);
        reader.Skip(12);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        throw new NotImplementedException();
    }

}