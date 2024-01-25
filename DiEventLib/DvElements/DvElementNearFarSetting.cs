using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvElementNearFarSetting : DvNodeObject
{
    public uint Field_00 {  get; set; }
    public float Near {  get; set; }
    public float Far { get; set; }
    public uint[] Field_10 { get; set; }
    public DvElementNearFarSetting() { }
    public DvElementNearFarSetting(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        Near = reader.Read<float>();
        Far = reader.Read<float>();
        Field_10 = reader.ReadArray<uint>(5);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.Write(Near);
        writer.Write(Far);
        writer.WriteArray(Field_10);
    }
}
