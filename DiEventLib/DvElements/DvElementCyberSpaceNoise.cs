using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvElementCyberSpaceNoise : DvNodeObject
{
    public uint Flags {  get; set; }
    public float[] CurveData {  get; set; }
    public DvElementCyberSpaceNoise() { }
    public DvElementCyberSpaceNoise(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Field_4f = reader.Read<uint>();
        Data = reader.ReadArray<float>(32);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_4f);
        writer.WriteArray(Data);
    }
}
