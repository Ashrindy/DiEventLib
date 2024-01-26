using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvElementChangeTimeScale : DvNodeObject
{
    public DvElementChangeTimeScale() { }
    public DvElementChangeTimeScale(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
    }

    public override void Write(BinaryObjectWriter writer)
    {
    }
}
