using Amicitia.IO.Binary;
using DvSceneLib.Misc;
using System.Text;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("UV Animation", "Adds .uv-anim into the cutscene")]
public class DvElementUVAnim : DvNodeElement
{
    public uint Flags = 0;
    public string FileName = "";
    public uint Field_44 = 0;
    public float Speed = 0;
    public uint Field_4c = 0;
    public uint Field_50 = 0;
    public DvElementUVAnim() : base(DvElementID.UVAnim) { }
    public DvElementUVAnim(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Flags = reader.Read<uint>();
        FileName = reader.ReadDvString(Utils.StringEncoding.ShiftJIS);
        Field_44 = reader.Read<uint>();
        Speed = reader.Read<float>();
        Field_4c = reader.Read<uint>();
        Field_50 = reader.Read<uint>();
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        writer.Write(Flags);
        writer.WriteDvString(FileName, Utils.StringEncoding.ShiftJIS);
        writer.Write(Field_44);
        writer.Write(Speed);
        writer.Write(Field_4c);
        writer.Write(Field_50);
    }
}
