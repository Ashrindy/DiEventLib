using Amicitia.IO.Binary;
using DiEventLib.Misc;
using System.Text;

namespace DiEventLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Visibility Animation", "Adds .vis-anim to the cutscene")]
public class DvElementVisibilityAnim : DvNodeElement
{
    public uint Field_00 = 0;
    public string FileName = "";
    public uint Field_44 = 0;
    public float Field_48 = 0;
    public uint Field_4c = 0;
    public uint Field_50 = 0;
    public DvElementVisibilityAnim() : base(DvElementID.VisibilityAnim) { }
    public DvElementVisibilityAnim(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        FileName = reader.ReadDvString(Utils.StringEncoding.ShiftJIS);
        Field_44 = reader.Read<uint>();
        Field_48 = reader.Read<float>();
        Field_4c = reader.Read<uint>();
        Field_50 = reader.Read<uint>();
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.WriteDvString(FileName, Utils.StringEncoding.ShiftJIS);
        writer.Write(Field_44);
        writer.Write(Field_48);
        writer.Write(Field_4c);
        writer.Write(Field_50);
    }
}
