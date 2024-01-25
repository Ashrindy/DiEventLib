using Amicitia.IO.Binary;
using System.Reflection.PortableExecutable;
using System.Text;

namespace DiEventLib;

public class DvElementCameraExposure : DvNodeObject
{
    public int unk1 { get; set; }
    public float[] Field_48 { get; set; }
    public float[] Field_80 { get; set; }

    public DvElementCameraExposure() { }
    public DvElementCameraExposure(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        unk1 = reader.Read<int>();
        Field_48 = reader.ReadArray<float>(7);
        Field_80 = reader.ReadArray<float>(32);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(unk1);
        writer.WriteArray(Field_48);
        writer.WriteArray(Field_80);
    }
}
