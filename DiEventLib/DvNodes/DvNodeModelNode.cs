using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvNodeModelNode : DvNode
{
    public uint Field00 = 0;
    public string ModelNodeName = "";

    public DvNodeModelNode() { }
    public DvNodeModelNode(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Field00 = reader.Read<uint>();
        NodeName = reader.ReadDvString(Utils.StringEncoding.ShiftJIS);
        reader.Skip(12);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field00);
        writer.WriteDvString(ModelNodeName, Utils.StringEncoding.ShiftJIS);
        writer.WriteNulls(12);
    }

}