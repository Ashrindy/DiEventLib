using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvElementChromaticAberration : DvNodeObject
{
    public float[] Data1 { get; set; }
    public float[] Data2 { get; set; }
    public DvElementChromaticAberration() { }
    public DvElementChromaticAberration(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Data1 = reader.ReadArray<float>(17);
        Data2 = reader.ReadArray<float>(32);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.WriteArray(Data1);
        writer.WriteArray(Data2);
    }
}
