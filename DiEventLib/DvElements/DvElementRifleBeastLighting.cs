using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvElementRifleBeastLighting : DvNodeObject
{
    public DvElementRifleBeastLighting() { }
    public DvElementRifleBeastLighting(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
    }

    public override void Write(BinaryObjectWriter writer)
    {
    }
}
