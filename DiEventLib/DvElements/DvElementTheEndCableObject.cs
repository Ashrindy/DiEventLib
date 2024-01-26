using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvElementTheEndCableObject : DvNodeObject
{
    public DvElementTheEndCableObject() { }
    public DvElementTheEndCableObject(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
    }

    public override void Write(BinaryObjectWriter writer)
    {
    }
}
