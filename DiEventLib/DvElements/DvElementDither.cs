using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvElementDither : DvNodeObject
{
    public float Param1 {  get; set; }
    public float Param2 {  get; set; }
    public DvElementDither() { }
    public DvElementDither(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Param1 = reader.Read<float>();
        Param2 = reader.Read<float>();
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Param1);
        writer.Write(Param2);
    }
}
