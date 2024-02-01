using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvElementOverrideASM : DvNodeObject
{
    public float[] Data { get; set; }
    public DvElementOverrideASM() { }
    public DvElementOverrideASM(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Data = reader.ReadArray<float>(28);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.WriteArray(Data);
    }
}
