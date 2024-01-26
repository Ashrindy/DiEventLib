using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvElementOverrideASM : DvNodeObject
{
    public DvElementOverrideASM() { }
    public DvElementOverrideASM(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
    }

    public override void Write(BinaryObjectWriter writer)
    {
    }
}
