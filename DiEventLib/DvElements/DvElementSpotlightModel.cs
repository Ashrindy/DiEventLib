using Amicitia.IO.Binary;
using System.Reflection.PortableExecutable;
using System.Text;

namespace DiEventLib;

public class DvElementSpotlightModel : DvNodeObject
{
    // TODO: FIND VALUES!!!
    public DvElementSpotlightModel() { }
    public DvElementSpotlightModel(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        reader.Skip(348);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.WriteNulls(348);
    }
}
